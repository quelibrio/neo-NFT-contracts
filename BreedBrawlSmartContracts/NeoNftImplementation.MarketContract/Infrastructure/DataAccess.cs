using System.Linq;
using System.Numerics;
using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using NeoNftImplementation.MarketContract.Handlers;
using NeoNftImplementation.MarketContract.Models;
using Helper = Neo.SmartContract.Framework.Helper;

namespace NeoNftImplementation.MarketContract.Infrastructure
{
    public class DataAccess : SmartContract
    {
        /// <summary>
        /// Get auction information
        /// </summary>
        public static object[] GetAuctionAsObjects(byte[] tokenId)
        {
            byte[] bytes = Storage.Get(Storage.CurrentContext, tokenId);
            if (bytes.Length == 0)
            {
                return new object[0];
            }

            return (object[])Helper.Deserialize(bytes);
        }

        /// <summary>
        /// Get auction transaction record
        /// </summary>
        public static object[] GetAuctionRecordAsObjects(byte[] auctionId)
        {
            var key = "auction".AsByteArray().Concat(auctionId);
            byte[] bytes = Storage.Get(Storage.CurrentContext, key);
            if (bytes.Length == 0)
            {
                return new object[0];
            }

            return (object[])Helper.Deserialize(bytes);
        }

        /// <summary>
        /// Get 0 generation gladiator price range
        /// </summary>
        public static object[] GetGenerationZeroPricesAsObjects()
        {
            byte[] bytes = Storage.Get(Storage.CurrentContext, Keys.GenerationZeroPricesKey);
            if (bytes.Length == 0)
            {
                return new object[0];
            }

            return (object[])Helper.Deserialize(bytes);
        }

        public static byte[] GetCGasScriptHashAsBytes() =>
            Storage.Get(Storage.CurrentContext, Keys.SGasKey);

        public static byte[] GetCGasBalanceAsBytes() =>
            Storage.Get(Storage.CurrentContext, Keys.CGasBalanceKey);

        public static byte[] GetTransactionAsBytes(byte[] id)
        {
            byte[] key = Keys.TransactionKey(id);
            return Storage.Get(Storage.CurrentContext, key);
        }

        public static byte[] GetUserBalanceAsBytes(byte[] address)
        {
            var key = Keys.UserBalanceKey(address);
            return Storage.Get(Storage.CurrentContext, key);
        }

        /// <summary>
        /// Store auction transaction records
        /// </summary>
        public static void _putAuctionRecord(byte[] tokenId, AuctionRecord info)
        {
            var key = "auction".AsByteArray().Concat(tokenId);
            byte[] bytes = Helper.Serialize(info);
            Storage.Put(Storage.CurrentContext, key, bytes);
        }

        /// <summary>
        /// Store 0 generation gladiator price range
        /// </summary>
        public static bool SetGenerationZeroPrice(GenerationZeroPrice model)
        {
            //Make a minimum judgment to prevent the setting too small
            if (model.MinPrice < AdminOperations.GenerationZeroMinPrice)
            {
                model.MaxPrice = AdminOperations.GenerationZeroMaxPrice;
                model.MinPrice = AdminOperations.GenerationZeroMinPrice;
                model.Duration = AdminOperations.GenerationZeroAuctionDuration;
            }

            byte[] bytes = Helper.Serialize(model);
            Storage.Put(Storage.CurrentContext, Keys.GenerationZeroPricesKey, bytes);

            return true;
        }

        public static void SetCGasBalance(BigInteger total) =>
            Storage.Put(Storage.CurrentContext, Keys.CGasBalanceKey, total);

        /// <summary>
        /// Store auction information
        /// </summary>
        public static void SetAuction(byte[] tokenId, MarketAuction model)
        {
            byte[] bytes = Helper.Serialize(model);
            Storage.Put(Storage.CurrentContext, tokenId, bytes);
        }

        /// <summary>
        /// Delete store auction information
        /// </summary>
        public static void DeleteAuction(byte[] tokenId) =>
            Storage.Delete(Storage.CurrentContext, tokenId);
    }
}
