using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace NeoNftProject.Notifications
{
	public class BuyOnAuctionNotification
	{
        //byte[] buyer, BigInteger tokenId, BigInteger curBuyPrice, BigInteger fee, BigInteger nowtime

        public byte[] Buyer { get; set; }

		public BigInteger TokenId { get; set; }

        public BigInteger CurrentBuyPrice { get; set; }

        public BigInteger Fee { get; set; }

        public BigInteger NowTime { get; set; }
    }
}
