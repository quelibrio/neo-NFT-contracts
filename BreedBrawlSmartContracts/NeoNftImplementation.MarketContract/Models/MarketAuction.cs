using System;
using System.Numerics;

namespace NeoNftImplementation.MarketContract.Models
{
    /// <summary>
    /// In the auction record in the auction
    /// </summary>
    [Serializable]
    public class MarketAuction
    {
        public byte[] Owner;
        // 0 auction 1 clone auction
        public int SellType;
        public uint SellTime;
        public BigInteger BeginPrice;
        public BigInteger EndPrice;
        public BigInteger Duration;
    }
}
