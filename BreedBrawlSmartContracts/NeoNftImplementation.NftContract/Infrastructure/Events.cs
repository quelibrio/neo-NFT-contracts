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

        public delegate void deleBirth(BigInteger tokenId, byte[] owner, BigInteger health, BigInteger mana, BigInteger agility, BigInteger stamina, BigInteger criticalStrike,
            BigInteger attackSpeed, BigInteger versatility, BigInteger mastery, BigInteger level);
        [DisplayName("birth")]
        public static event deleBirth Birthed;
        public static void RaiseBirthed(BigInteger tokenId,byte[] owner, BigInteger health, BigInteger mana, BigInteger agility, BigInteger stamina, BigInteger criticalStrike, 
            BigInteger attackSpeed, BigInteger versatility, BigInteger mastery, BigInteger level) => Birthed(
                tokenId, owner, health, mana, agility, stamina, criticalStrike, attackSpeed, versatility, mastery, level);

        public delegate void deleGladiatorCloned(byte[] owner, BigInteger motherId, BigInteger motherCd, BigInteger fatherId, BigInteger fatherCd);
        [DisplayName("gladiatorCloned")]
        public static event deleGladiatorCloned TokenCloned;
        public static void RaiseTokenCloned(byte[] owner, BigInteger motherId, BigInteger motherCd, BigInteger fatherId, BigInteger fatherCd) =>
            TokenCloned(owner, motherId, motherCd, fatherId, fatherCd);
    }
}
