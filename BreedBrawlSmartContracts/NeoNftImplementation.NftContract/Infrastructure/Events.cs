using System.Numerics;
using System.ComponentModel;
using Neo.SmartContract.Framework;

namespace NeoNftImplementation.NftContract.Infrastructure
{
    public class Events: SmartContract
    {
        public delegate void deleTransfer(byte[] from, byte[] to, BigInteger value);
        [DisplayName("transfer")]
        public static event deleTransfer Transferred;
        public static void RaiseTransfer(byte[] from, byte[] to, BigInteger value) => Transferred(from, to, value);

        public delegate void deleApproved(byte[] owner, byte[] approved, BigInteger tokenId);
        [DisplayName("approve")]
        public static event deleApproved Approved;
        public static void RaiseApproved(byte[] owner, byte[] approved, BigInteger tokenId) => Approved(owner, approved, tokenId);

        public delegate void deleBirth(BigInteger tokenId, byte[] owner, BigInteger agility, BigInteger attackSpeed, BigInteger criticalStrike, BigInteger nextActionAt,
            BigInteger cloneWithId, BigInteger birthTime, BigInteger matronId, BigInteger sireId, BigInteger generation);
        [DisplayName("birth")]
        public static event deleBirth Birthed;
        public static void RaiseBirthed(BigInteger tokenId, byte[] owner, BigInteger agility, BigInteger attackSpeed, BigInteger criticalStrike, BigInteger nextActionAt,
            BigInteger cloneWithId, BigInteger birthTime, BigInteger matronId, BigInteger sireId, BigInteger generation) => Birthed(
                tokenId, owner, agility, attackSpeed, criticalStrike, nextActionAt,
                cloneWithId, birthTime, matronId, sireId, generation);

        public delegate void deleGladiatorCloned(byte[] owner, BigInteger motherId, BigInteger motherCd, BigInteger fatherId, BigInteger fatherCd);
        [DisplayName("gladiatorCloned")]
        public static event deleGladiatorCloned TokenCloned;
        public static void RaiseTokenCloned(byte[] owner, BigInteger motherId, BigInteger motherCd, BigInteger fatherId, BigInteger fatherCd) =>
            TokenCloned(owner, motherId, motherCd, fatherId, fatherCd);
    }
}
