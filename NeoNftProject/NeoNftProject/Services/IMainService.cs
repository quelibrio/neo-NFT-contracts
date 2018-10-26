using NeoNftProject.Data;
using NeoNftProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeoNftProject.Services
{
	public interface IMainService
	{
		ICollection<Auction> GetAuctions();
		ICollection<Token> GetOwnedTokens(string owner);
		int CreateAuction(CreateAuctionInputModel model);
		Auction UpdateAuction(UpdateAuctionModel model);
		Token UpdateToken(UpdateTokenInputModel model);
		int CreateToken(CreateTokenInputModel token);
		int CreateAddress(string address);
		int CreateTransaction(CreateTransactionInputModel model);
	}
}
