using Akka.Actor;
using Microsoft.EntityFrameworkCore;
using Neo;
using NeoNftProject.Data;
using NeoNftProject.Extensions;
using NeoNftProject.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using static Neo.Ledger.Blockchain;

namespace NeoNftProject.Actors
{
	public class NotificationsReader : UntypedActor
	{
		private readonly string connectionString;

		public NotificationsReader(IActorRef blockchain, string connectionString)
		{
			blockchain.Tell(new Register());
			this.connectionString = connectionString;
		}

		protected override void OnReceive(object message)
		{
			if (message is ApplicationExecuted m)
			{
				var optionsBuilder = new DbContextOptionsBuilder<NeoNftContext>();
				optionsBuilder.UseSqlServer(connectionString);
				var db = new NeoNftContext(optionsBuilder.Options);

				foreach (var result in m.ExecutionResults)
				{
					foreach (var notification in result.Notifications)
					{
						var type = notification.GetNotificationType();
						if (type == "transfer")
						{
                            TransferNotification transferNotification = notification.GetNotification<TransferNotification>();
                            string receiverHexString = transferNotification.To.ToHexString();
                            string senderHexString = transferNotification.From.ToHexString();
                            BigInteger tokenId = transferNotification.TokenId;


                            Data.Address receiver = db.Addresses.FirstOrDefault(c => c.AddressName == receiverHexString);
                            Data.Address sender = db.Addresses.FirstOrDefault(c => c.AddressName == senderHexString);
                            Token token = db.Tokens.FirstOrDefault(c => c.TxId == tokenId.ToString());

                            var transaction = new Transaction();
                            transaction.Receiver = receiver;
                            transaction.Sender = sender;
                            token.Address = receiver;

                            db.Add(transaction);
                            db.Update(token);
                            db.SaveChanges();
						}
                        else if (type == "birth")
                        {
                            MintTokenNotification mintTokenNotification = notification.GetNotification<MintTokenNotification>();
                            Token token = new Token();
                            string owner = mintTokenNotification.Owner.ToHexString();
                            token.TxId = mintTokenNotification.TokenId.ToString();
                            token.Nickname = "Olaf";
                            token.Health = mintTokenNotification.Health;
                            token.Mana = mintTokenNotification.Mana;
                            token.Agility = mintTokenNotification.Agility;
                            token.Stamina = mintTokenNotification.Stamina;
                            token.CriticalStrike = mintTokenNotification.CriticalStrike;
                            token.AttackSpeed = mintTokenNotification.AttackSpeed;
                            token.Versatility = mintTokenNotification.Versatility;
                            token.Mastery = mintTokenNotification.Mastery;
                            token.Level = mintTokenNotification.Level;
                            token.Experience = 0;
                            Data.Address address = db.Addresses.FirstOrDefault(c =>  owner == c.AddressName);
                            if (address == null)
                            {
                                address = new Data.Address();
                                address.AddressName = owner;
                                db.Add(address);
                            }

                            token.Address = address;
                            db.Add(token);
                            db.SaveChanges();
                        }
                        else if (type == "auction")
                        {
                            CreateSaleAuctionNotification auctionCreatedNotification = notification.GetNotification<CreateSaleAuctionNotification>();
                            Auction auction = new Auction();
                            auction.StartDate = DateTime.Now;
                            auction.StartPrice = (decimal)auctionCreatedNotification.BeginPrice;
                            auction.IsActive = 1;
                            auction.EndPrice = (decimal)auctionCreatedNotification.EndPrice;
                            auction.Duration = (long)auctionCreatedNotification.Duration;
                            Token token = db.Tokens.FirstOrDefault(c => c.TxId == auctionCreatedNotification.TokenId.ToString());

                            if (token != null)
                            {
                                auction.Token = token;
                                db.Auctions.Add(auction);
                                db.SaveChanges();

                            }
                        }
                        else if (type == "cancelAuction")
                        {
                            CancelAuctionNotification cancelAuctionNotification = notification.GetNotification<CancelAuctionNotification>();
                            Token token = db.Tokens.Include(c=> c.Address).FirstOrDefault(c => c.TxId == cancelAuctionNotification.TokenId.ToString() && c.Address.AddressName == cancelAuctionNotification.TokenOwner.ToHexString());
                            if (token != null)
                            {
                                Auction auction = db.Auctions.FirstOrDefault(c => c.Token == token);
                                auction.EndDate = DateTime.Now;
                                auction.IsActive = 0;
                                db.Update(auction);
                                db.SaveChanges();

                            }

                        }
                        else if (type == "auctionBuy")
                        {
                            BuyOnAuctionNotification buyOnAuctionNotification = notification.GetNotification<BuyOnAuctionNotification>();

                            Auction auction = db.Auctions.FirstOrDefault(c => c.TokenId == buyOnAuctionNotification.TokenId && c.IsActive == 1);
                            Token token = db.Tokens.FirstOrDefault(c => c.TxId == buyOnAuctionNotification.TokenId.ToString());
                            Data.Address address = db.Addresses.FirstOrDefault(c => c.AddressName == buyOnAuctionNotification.Buyer.ToHexString());

                            if (token != null && auction != null && address != null)
                            {
                                if (token.Address != address)
                                {
                                    auction.IsActive = 0;
                                    auction.EndPrice = (decimal)buyOnAuctionNotification.CurrentBuyPrice;
                                    auction.CurrentPrice = (decimal)buyOnAuctionNotification.CurrentBuyPrice;
                                    token.Address = address;

                                    db.Update(auction);
                                    db.Update(token);
                                    db.SaveChanges();
                                }
                            }
                        }

					}
				}

				db.Dispose();
			}
		}

		public static Props Props(IActorRef blockchain, string connectionString)
		{

			return Akka.Actor.Props.Create(() => new NotificationsReader(blockchain, connectionString));
		}
	}
}
