using System.Numerics;
using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using Neo.SmartContract.Framework.Services.System;
using NeoNftImplementation.NftContract.Models;
using Helper = Neo.SmartContract.Framework.Helper;

namespace NeoNftImplementation.NftContract.Infrastructure
{
    public class DataAccess : SmartContract
    {
        public static TokenInfo GetToken(byte[] id)
        {
            byte[] key = Keys.Token(id);
            byte[] bytes = Storage.Get(Storage.CurrentContext, id);
            if (bytes.Length == 0)
            {
                return null;
            }

            return (TokenInfo)(object)(object[])Helper.Deserialize(bytes);
        }

        public static TransferInfo GetTransaction(byte[] id)
        {
            byte[] bytes = Storage.Get(Storage.CurrentContext, id);
            if (bytes.Length == 0)
            {
                return null;
            }

            return (TransferInfo)(object)(object[])Helper.Deserialize(bytes);
        }

        public static object[] GetTokenAsObjects(byte[] id)
        {
            byte[] key = Keys.Token(id);
            byte[] bytes = Storage.Get(Storage.CurrentContext, id);
            if (bytes.Length == 0)
            {
                return new object[0];
            }

            return (object[])Helper.Deserialize(bytes);
        }

        /// <summary>
        /// Get skill weapon attribute configuration parameters
        /// </summary>
        public static object[] GetAttrConfigAsObjects()
        {
            byte[] bytes = Storage.Get(Storage.CurrentContext, Keys.AttributeConfig);
            if (bytes.Length == 0)
            {
                return new object[0];
            }

            return (object[])Helper.Deserialize(bytes);
        }

        public static object[] GetTransactionInfoAsObjects(byte[] id)
        {
            byte[] bytes = Storage.Get(Storage.CurrentContext, id);
            if (bytes.Length == 0)
            {
                return new object[0];
            }

            return (object[])Helper.Deserialize(bytes);
        }

        /// <summary>
        /// Get the index of the first gladiator information owned by an address.
        /// </summary>
        public static byte[] GetOwnersTokenIdByIndexAsBytes(byte[] owner, BigInteger index)
        {
            byte[] key = Keys.TokenOfOwner(owner, index);
            return Storage.Get(Storage.CurrentContext, key);
        }

        public static byte[] GetMarketAddressAsBytes() =>
            Storage.Get(Storage.CurrentContext, Keys.MarketAddress);

        public static byte[] GetTotalSupplyAsBytes() =>
            Storage.Get(Storage.CurrentContext, Keys.KeyTotalSupply);

        public static byte[] GetApprovedAddressAsBytes(byte[] tokenId)
        {
            byte[] key = Keys.Approval(tokenId);
            return Storage.Get(Storage.CurrentContext, key);            
        }

        public static void SetApprovedAddress(byte[] tokenId, byte[] address)
        {
            byte[] key = Keys.Approval(tokenId);
            Storage.Put(Storage.CurrentContext, key, address);
        }

        public static void SetAttributeConfig(AttributeConfig config)
        {
            byte[] bytes = Helper.Serialize(config);
            Storage.Put(Storage.CurrentContext, Keys.AttributeConfig, bytes);
        }

        public static void SetTotalSupply(byte[] totalSupply) =>
            Storage.Put(Storage.CurrentContext, Keys.KeyTotalSupply, totalSupply);

        public static void SetToken(byte[] id, TokenInfo token)
        {
            byte[] key = Keys.Token(id);
            byte[] bytes = Helper.Serialize(token);
            Storage.Put(Storage.CurrentContext, key, bytes);
        }

        public static void SetTransactionInfo(byte[] from, byte[] to, BigInteger value)
        {
            TransferInfo info = new TransferInfo { from = from,  to = to, value = value };

            byte[] rawTransaction = Helper.Serialize(info);
            byte[] id = (ExecutionEngine.ScriptContainer as Transaction).Hash;

            Storage.Put(Storage.CurrentContext, id, rawTransaction);
        }

        public static void IncreaseAddressBalance(byte[] address)
        {
            byte[] key = Keys.AddressBalanceKey(address);
            byte[] currentBalanceBytes = Storage.Get(Storage.CurrentContext, key);
            BigInteger currentBalance = 0;
            if (currentBalanceBytes.Length != 0)
            {
                currentBalance = currentBalanceBytes.AsBigInteger();
            }
            
            currentBalance += 1;
            Storage.Put(Storage.CurrentContext, address, currentBalance.AsByteArray());
        }

        public static void DecreaseAddressBalance(byte[] address)
        {
            byte[] key = Keys.AddressBalanceKey(address);
            byte[] currentBalanceBytes = Storage.Get(Storage.CurrentContext, key);
            BigInteger currentBalance = 0;
            if (currentBalanceBytes.Length != 0)
            {
                currentBalance = currentBalanceBytes.AsBigInteger();
            }
            
            currentBalance -= 1;
            if (currentBalance < 0)
            {
                currentBalance = 0;
            }

            Storage.Put(Storage.CurrentContext, address, currentBalance.AsByteArray());
        }

        public static void RemoveApproval(byte[] tokenId)
        {
            byte[] key = Keys.Approval(tokenId);
            Storage.Delete(Storage.CurrentContext, key);
        }
    }
}
