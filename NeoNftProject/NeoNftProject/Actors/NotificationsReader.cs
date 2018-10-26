using Akka.Actor;
using Microsoft.EntityFrameworkCore;
using Neo;
using NeoNftProject.Data;
using NeoNftProject.Extensions;
using NeoNftProject.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
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
							var transferNotification = notification.GetNotification<TransferNotification>();
                            var receiverHexString = transferNotification.To.ToHexString();
                            var senderHexString = transferNotification.From.ToHexString();
                            var tokenId = transferNotification.TokenId;


                            var receiver = db.Addresses.FirstOrDefault(c => c.AddressName == receiverHexString);
                            var sender = db.Addresses.FirstOrDefault(c => c.AddressName == senderHexString);
                            var token = db.Tokens.FirstOrDefault(c => c.TxId == tokenId.ToString());

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
                            var mintTokenNotification = notification.GetNotification<MintTokenNotification>();
                            var token = new Token(); 
                            token.Agility = (int)mintTokenNotification.Agility;
                            token.AttackSpeed = (int)mintTokenNotification.AttackSpeed;
                            token.CriticalStrike = (int)mintTokenNotification.CriticalStrike;

                            db.Add(token);
                            db.SaveChanges();
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
