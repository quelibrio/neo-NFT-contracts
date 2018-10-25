using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeoNftProject.Data
{
	public class Transaction
	{
		public int Id { get; set; }

		public int SenderId { get; set; }

		public int ReceiverId { get; set; }

		public Address Sender { get; set; }

		public Address Receiver { get; set; }
	}
}
