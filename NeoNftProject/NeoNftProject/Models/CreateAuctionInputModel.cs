using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeoNftProject.Models
{
	public class CreateAuctionInputModel
	{
		public string StartDate { get; set; }

		public string EndDate { get; set; }

		public decimal CurrentPrice { get; set; }

		public decimal StartPrice { get; set; }

		public int TokenId { get; set; }

		public int AddressId { get; set; }
}
}
