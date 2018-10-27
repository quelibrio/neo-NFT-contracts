using System;
using System.Numerics;

namespace NeoNftImplementation.NftContract.Models
{
    /// <summary>
    /// Gladiator attribute structure data
    /// </summary>
    [Serializable]
    public class TokenInfo
    {
        public byte[] Owner;

        public int IsBreeding;

        public BigInteger CanBreedAfter; //uint256 next time or birth time
        public BigInteger CloneWithId;//uint256 If the child is pregnant, it is the husband id, otherwise=0
        public BigInteger BirthTime; //uint256 birth time, timestamp
        public BigInteger MotherId; //uint256 mother id, initial 0
        public BigInteger FatherId; //uint256 father id, the initial generation is 0
        public BigInteger Level; //uint 256 generation
        public BigInteger CooldownLevel;
        public int IsBrawling;

        public BigInteger Health;
        public BigInteger Mana;
        public BigInteger Agility;
        public BigInteger Stamina;
        public BigInteger CriticalStrike;
        public BigInteger AttackSpeed;
        public BigInteger Versatility;
        public BigInteger Mastery;

    }
}
