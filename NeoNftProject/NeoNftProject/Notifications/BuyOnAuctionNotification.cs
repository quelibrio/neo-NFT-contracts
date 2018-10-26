using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace NeoNftProject.Notifications
{
	public class BuyOnAuctionNotification
	{
		public byte[] Sender { get; set; }

		public BigInteger TokenId { get; set; }
	}
}
