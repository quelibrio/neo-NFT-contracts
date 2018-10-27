using System.Numerics;

namespace NeoNftImplementation.MarketContract.Models
{
    /// <summary>
    /// Auction transaction record
    /// </summary>
    public class AuctionRecord
    {
        public BigInteger TokenId;
        public byte[] Seller;
        public byte[] Buyer;
        public int SellType;
        public BigInteger SellPrice;
        public BigInteger SellTime;
    }
}
