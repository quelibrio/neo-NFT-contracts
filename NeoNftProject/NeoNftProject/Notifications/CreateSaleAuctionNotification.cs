﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace NeoNftProject.Notifications
{
	public class CreateSaleAuctionNotification
	{

        public byte[] Owner { get; set; }
        public BigInteger TokenId { get; set; }
        public BigInteger BeginPrice { get; set; }
        public BigInteger EndPrice { get; set; }
        public BigInteger Duration { get; set; }
        public int SellType { get; set; }
        public uint SellTime { get; set; }


    }
}
