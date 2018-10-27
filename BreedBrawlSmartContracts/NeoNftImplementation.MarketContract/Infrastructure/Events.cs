using System.ComponentModel;
using System.Numerics;

using Neo.SmartContract.Framework;

namespace NeoNftImplementation.MarketContract.Infrastructure
{
    public class Events : SmartContract
    {
        public delegate void deleAuction(byte[] owner, BigInteger tokenId, BigInteger beginPrice, BigInteger endPrice, BigInteger duration, int sellType, uint sellTime);
        [DisplayName("auction")]
        public static event deleAuction AuctionCreated;
        public static void RaiseAuctioned(byte[] owner, BigInteger tokenId, BigInteger beginPrice, BigInteger endPrice, BigInteger duration, int sellType, uint sellTime) =>
            AuctionCreated(owner, tokenId, beginPrice, endPrice, duration, sellType, sellTime);

        public delegate void deleCancelAuction(byte[] owner, BigInteger tokenId);
        [DisplayName("cancelAuction")]
        public static event deleCancelAuction CancelAuctioned;
        public static void RaiseCancelAuctioned(byte[] owner, BigInteger tokenId) =>
            CancelAuctioned(owner, tokenId);

        public delegate void deleAuctionBuy(byte[] buyer, BigInteger tokenId, BigInteger curBuyPrice, BigInteger fee, BigInteger nowtime);
        [DisplayName("auctionBuy")]
        public static event deleAuctionBuy AuctionBuy;
        public static void RaiseAuctionBuy(byte[] buyer, BigInteger tokenId, BigInteger curBuyPrice, BigInteger fee, BigInteger nowtime) =>
            AuctionBuy(buyer, tokenId, curBuyPrice, fee, nowtime);

        public delegate void deleAuctionClone(byte[] buyer, BigInteger motherId, BigInteger fatherId, BigInteger curBuyPrice, BigInteger fee, BigInteger nowtime);
        [DisplayName("auctionClone")]
        public static event deleAuctionClone AuctionClone;
        public static void RaiseAuctionClone(byte[] buyer, BigInteger motherId, BigInteger fatherId, BigInteger curBuyPrice, BigInteger fee, BigInteger nowtime) =>
            AuctionClone(buyer, motherId, fatherId, curBuyPrice, fee, nowtime);

        public delegate void deleMyClone(byte[] owner, BigInteger motherId, BigInteger fatherId, BigInteger fee, BigInteger nowtime);
        [DisplayName("myClone")]
        public static event deleMyClone MyClone;
        public static void RaiseMyClone(byte[] owner, BigInteger motherId, BigInteger fatherId, BigInteger fee, BigInteger nowtime) =>
            MyClone(owner, motherId, fatherId, fee, nowtime);
    }
}
