using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Neo.Ledger;
using NeoNftProject.Models;
using NeoNftProject.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NeoNftProject.Controllers
{
	[Route("api")]
	public class MainController : Controller
	{

		private IMainService mainService;
		public MainController(IMainService service)
		{
			this.mainService = service;
		}

		[HttpGet("auction")]
		public IActionResult Auctions()
		{
			return this.Ok(this.mainService.GetAuctions());
		}

		[HttpPost("auction")]
		public IActionResult CreateAuction([FromBody]CreateAuctionInputModel model)
		{
			var id = this.mainService.CreateAuction(model);

			if (id == 0)
			{
				return this.NotFound("The token doesn't exist.");
			}

			if (id == -1)
			{
				return this.NotFound("The address doesn't exist.");
			}

			return this.Ok(id);
		}

		[HttpPatch("auction")]
		public IActionResult UpdateAuction([FromBody]UpdateAuctionModel model)
		{
			var auction = this.mainService.UpdateAuction(model);

			if (auction == null)
			{
				return this.NotFound("The auction doesn't exist.");
			}

			return this.Ok(auction);
		}

		[HttpPost("address")]
		public IActionResult CreateAddress([FromBody]CreateAddressInputModel model)
		{
			var id = this.mainService.CreateAddress(model.Address);

			return this.Ok(id);
		}

		[HttpPost("token")]
		public IActionResult CreateToken([FromBody]CreateTokenInputModel model)
		{
			var id = this.mainService.CreateToken(model);

			if (id == 0)
			{
				return this.NotFound("The address doesn't exist.");
			}

			return this.Ok(id);
		}

		[HttpPatch("token")]
		public IActionResult UpdateToken([FromBody]UpdateTokenInputModel model)
		{
			var token = this.mainService.UpdateToken(model);

			if (token == null)
			{
				return this.NotFound("The token doesn't exist.");
			}

			return this.Ok(token);
		}

		[HttpGet("ownedTokens")]
		public IActionResult OwnedToken(string address)
		{
			var tokens = this.mainService.GetOwnedTokens(address);
			return this.Ok(tokens);
		}

		[HttpPost("transaction")]
		public IActionResult CreateTransaction([FromBody]CreateTransactionInputModel model)
		{
			var id = this.mainService.CreateTransaction(model);

			return this.Ok(id);
		}

		[HttpGet("height")]
		public IActionResult Height()
		{
			return this.Ok(Blockchain.Singleton.HeaderHeight);
		}
	}
}
