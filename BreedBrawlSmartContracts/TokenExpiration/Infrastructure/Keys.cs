using Neo.SmartContract.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TransfairExpiration
{
    public class Keys : SmartContract
    {
        public const string KeyTotalSupply = "totalSupply";

        public const string KeyAllSupply = "allSupply";

        public static byte[] Token(byte[] id) =>
            nameof(Token).AsByteArray().Concat(id);

        public static byte[] AddressBalanceKey(byte[] address) =>
            nameof(AddressBalanceKey).AsByteArray().Concat(address);

        public static byte[] TokenOfOwner(byte[] owner, BigInteger idx) =>
            owner.Concat(idx.AsByteArray());

        public static byte[] Approval(byte[] tokenId) =>
            nameof(Approval).AsByteArray().Concat(tokenId);
    }
}
