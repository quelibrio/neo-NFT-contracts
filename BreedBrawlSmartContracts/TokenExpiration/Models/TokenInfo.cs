using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TransfairExpiration
{
    /// <summary>
    /// Gladiator attribute structure data
    /// </summary>
    [Serializable]
    public class TokenInfo
    {
        public byte[] Owner;
        public byte[] OriginalOwner;
        public BigInteger LendExpiration;
    }
}
