using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NeoNftProject.Data;
using NeoNftProject.Models;

namespace NeoNftProject.Services
{
	public class MainService : IMainService
	{
		private readonly NeoNftContext db;

		public MainService(NeoNftContext db)
		{
			this.db = db;
		}

		public int CreateAddress(string address)
		{
			var addressObj = db.Addresses.FirstOrDefault(c => c.AddressName == address);

			if (addressObj == null)
			{
				var newAddress = new Address() { AddressName = address };
				db.Addresses.Add(newAddress);
				db.SaveChanges();

				return newAddress.Id;
			}
			else
			{
				return addressObj.Id;
			}

		}

		public int CreateAuction(Auction auction)
		{
			db.Auctions.Add(auction);
			db.SaveChanges();

			return auction.Id;
		}

		public int CreateAuction(CreateAuctionInputModel auction)
		{
			var span = DateTime.Parse(auction.EndDate).Subtract(DateTime.Parse(auction.StartDate));
			var duration = (long)span.TotalMinutes;

			var newAuction = new Auction() {
						StartDate = DateTime.Parse(auction.StartDate),
						EndDate = DateTime.Parse(auction.EndDate),
						CurrentPrice = auction.StartPrice,
						StartPrice = auction.StartPrice,
						Duration = duration,
						IsActive = 1,
						EndPrice = 0 };

			var token = db.Tokens.FirstOrDefault(c => c.Id == auction.TokenId);

			if (token == null)
			{
				return 0;
			}

			var address = db.Addresses.FirstOrDefault(c => c.Id == auction.AddressId);
			if (address == null)
			{
				return -1;
			}

			newAuction.Token = token;
			newAuction.Address = address;
			db.Add(newAuction);
			db.SaveChanges();

			return newAuction.Id;
		}

		public int CreateToken(CreateTokenInputModel token)
		{
			var newToken = new Token() { Health = token.Health,
							AttackSpeed = token.AttackSpeed,
							Agility = token.Agility, ImageUrl= token.ImageUrl,
							CriticalStrike = token.CriticalStrike,
							Mana = token.Mana,
							Mastery = token.Mastery,
							Nickname = token.Nickname,
							Stamina = token.Stamina,
							Versatility = token.Versatility,
							TxId = token.TxId,
							IsBrawling = token.IsBrawling,
							IsBreeding = token.IsBreeding,
							Level = token.Level,
							Experience = token.Experience };

			var address = db.Addresses.FirstOrDefault(c => c.Id == token.AddressId);

			if (address == null)
			{
				return 0;
			}

			newToken.Address = address;
			db.Add(newToken);
			db.SaveChanges();

			return newToken.Id;
		}

		public int CreateTransaction(CreateTransactionInputModel model)
		{
			var transaction = new Transaction();
			var receiver = db.Addresses.FirstOrDefault(c => c.Id == model.ReceiverId);
			var sender = db.Addresses.FirstOrDefault(c => c.Id == model.SenderId);
			var token = db.Tokens.FirstOrDefault(c => c.Id == model.TokenId);

			if (receiver == null) return 0;
			if (sender == null) return -1;
			if (token == null) return -2;

			token.Address = receiver;
			transaction.Receiver = receiver;
			transaction.Sender = sender;

			db.Add(transaction);
			db.Update(token);
			db.SaveChanges();

			return transaction.Id;

		}

		public ICollection<Auction> GetAuctions()
		{
			return this.db.Auctions
				.Where(c=> c.IsActive == 1)
				.ToList();
		}

		public ICollection<Token> GetOwnedTokens(string owner)
		{
            var tokens = db.Tokens.Where(c => c.Address.AddressName == owner).ToList();

			return tokens;
		}

		public Auction UpdateAuction(UpdateAuctionModel model)
		{
			var auction = db.Auctions.FirstOrDefault(c => c.Id == model.AuctionId);

			if (auction == null) return null;


			auction.CurrentPrice = model.CurrentPrice;

			if (model.IsSold != null)
			{
				auction.IsActive = 0;
				auction.EndPrice = (decimal)model.EndPrice;
			}

			db.Update(auction);
			db.SaveChanges();

			return auction;
		}

		public Token UpdateToken(UpdateTokenInputModel model)
		{
			var token = db.Tokens.FirstOrDefault(c => c.Id == model.Id);
			if (token == null) return null;

			// Manual map
			token.AddressId = (model.AddressId != 0 && model.AddressId != null)?  (int)model.AddressId : token.AddressId;
			token.Nickname = (model.Nickname != string.Empty) ? model.Nickname : token.Nickname;
			token.TxId = (model.TxId != string.Empty) ? model.TxId : token.TxId;
			token.Health = (model.Health != 0 && model.Health != null) ? (int)model.Health : token.Health;
			token.Mana = (model.Mana != 0 && model.Mana != null) ? (int)model.Mana : token.Mana;
			token.Agility = (model.Agility != 0 && model.Agility != null) ? (int)model.Agility : token.Agility;
			token.Stamina = (model.Stamina != 0 && model.Stamina != null) ? (int)model.Stamina : token.Stamina;
			token.CriticalStrike = (model.CriticalStrike != 0 && model.CriticalStrike != null) ? (int)model.CriticalStrike : token.CriticalStrike;
			token.AttackSpeed = (model.AttackSpeed != 0 && model.AttackSpeed != null) ? (int)model.AttackSpeed : token.AttackSpeed;
			token.Mastery = (model.Mastery != 0 && model.Mastery != null) ? (int)model.Mastery : token.Mastery;
			token.Versatility = (model.Versatility != 0 && model.Versatility != null) ? (int)model.Versatility : token.Versatility;
			token.Experience = (model.Experience != 0 && model.Experience != null) ? (int)model.Experience : token.Experience;
			token.Level = (model.Level != 0 && model.Level != null) ? (int)model.Level : token.Level;
			token.ImageUrl = (model.ImageUrl != string.Empty) ? model.ImageUrl : token.ImageUrl;
			token.IsBreeding = (model.IsBreeding) ? model.IsBreeding : token.IsBreeding;
			token.IsBrawling = (model.IsBreeding) ? model.IsBrawling : token.IsBrawling;

			db.Update(token);
			db.SaveChanges();

			return token;
		}
	}
}