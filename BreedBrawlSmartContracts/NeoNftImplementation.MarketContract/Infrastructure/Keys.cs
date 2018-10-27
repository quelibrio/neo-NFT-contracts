using System.Linq;
using Neo.SmartContract.Framework;

namespace NeoNftImplementation.MarketContract.Infrastructure
{
    public class Keys : SmartContract
    {
        public const string CGasBalanceKey = "cgasBalance";

        public const string SGasKey = "sgas";

        public const string GenerationZeroPricesKey = "geno_p";

        public static byte[] TransactionKey(byte[] id) =>
            new byte[] { 0x11 }.Concat(id);

        public static byte[] UserBalanceKey(byte[] owner) =>
            new byte[] { 0x11 }.Concat(owner);
    }
}
