using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NeoNftProject.Data
{
	public class Address
	{

		public Address()
		{
			this.OutgoingTransactions = new List<Transaction>();
			this.IncomingTransactions = new List<Transaction>();
			this.Auctions = new List<Auction>();
		}

		public int Id { get; set; }

		public string AddressName { get; set; }


		public ICollection<Auction> Auctions { get; set; }

		[InverseProperty(nameof (Transaction.Sender))]
		public ICollection<Transaction> OutgoingTransactions { get; set; }

		[InverseProperty(nameof(Transaction.Receiver))]
		public ICollection<Transaction> IncomingTransactions { get; set; }
	}
}
