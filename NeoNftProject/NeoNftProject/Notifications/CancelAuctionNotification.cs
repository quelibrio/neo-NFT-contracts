using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace NeoNftProject.Notifications
{
	public class CancelAuctionNotification
	{
		public byte[] TokenOwner { get; set; }

		public BigInteger TokenId { get; set; }

	}
}
