using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeoNftProject.Models
{
	public class CreateTransactionInputModel
	{

		public int SenderId { get; set; }

		public int ReceiverId { get; set; }

		public int TokenId { get; set; }
	}
}
