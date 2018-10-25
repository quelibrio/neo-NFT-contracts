using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeoNftProject.Data
{
	public class Auction
	{
		public int Id { get; set; }

		public DateTime StartDate { get; set; }

		public DateTime EndDate { get; set; }

		public decimal CurrentPrice { get; set; }

		public decimal StartPrice { get; set; }

		public decimal EndPrice { get; set; }

		public long Duration { get; set; }

		public int TokenId { get; set; }

		public int AddressId { get; set; }

		public int IsActive { get; set; }

		public Address Address { get; set; }

		public Token Token { get; set; }
	}
}
