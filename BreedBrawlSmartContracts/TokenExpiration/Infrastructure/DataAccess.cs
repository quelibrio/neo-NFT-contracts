using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TransfairExpiration
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
            return (TokenInfo)(object)(object[])Neo.SmartContract.Framework.Helper.Deserialize(bytes);
        }

        /// <summary>
        /// Get the index of the first gladiator information owned by an address.
        /// </summary>
        public static byte[] GetOwnersTokenIdByIndexAsBytes(byte[] owner, BigInteger index)
        {
            byte[] key = Keys.TokenOfOwner(owner, index);
            return Storage.Get(Storage.CurrentContext, key);
        }

        public static byte[] GetTotalSupplyAsBytes() =>
            Storage.Get(Storage.CurrentContext, Keys.KeyTotalSupply);

        public static byte[] GetApprovedAddressAsBytes(byte[] tokenId)
        {
            byte[] key = Keys.Approval(tokenId);
            return Storage.Get(Storage.CurrentContext, key);
        }

        public static void SetTotalSupply(byte[] totalSupply) =>
            Storage.Put(Storage.CurrentContext, Keys.KeyTotalSupply, totalSupply);

        public static void SetToken(byte[] id, TokenInfo token)
        {
            byte[] key = Keys.Token(id);
            byte[] bytes = Neo.SmartContract.Framework.Helper.Serialize(token);
            Storage.Put(Storage.CurrentContext, key, bytes);
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

        public static void RemoveApproval(byte[] tokenId)
        {
            byte[] key = Keys.Approval(tokenId);
            Storage.Delete(Storage.CurrentContext, key);
        }
    }
}
