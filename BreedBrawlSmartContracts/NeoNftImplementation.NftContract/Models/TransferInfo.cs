using System.Numerics;

namespace NeoNftImplementation.NftContract.Models
{
    /// <summary>
    /// Gladiator transaction record
    /// </summary>
    public class TransferInfo
    {
        public byte[] from;
        public byte[] to;
        public BigInteger value;
    }
}
