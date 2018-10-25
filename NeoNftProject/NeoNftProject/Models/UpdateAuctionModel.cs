using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeoNftProject.Models
{
	public class UpdateAuctionModel
	{
		public int AuctionId { get; set; }

		public decimal CurrentPrice { get; set; }

		public decimal? EndPrice { get; set; }

		public bool? IsSold { get; set; }
	}
}
