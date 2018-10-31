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

        public int IsPregnant;
        public int IsReady; //bool can be pregnant (not in cd)

        public BigInteger ApprovalExpiration;

        public BigInteger CooldownLevel; //uint256 base cd (when parents are conceived, they can be triggered at the same time. If the parent cd arrives, they can be father or mother again; if the mother cd arrives, they can have children, and they can be father or mother again after birth.)
        public BigInteger CanBreedAfter; //uint256 next time or birth time
        public BigInteger CloneWithId;//uint256 If the child is pregnant, it is the husband id, otherwise=0
        public BigInteger BirthTime; //uint256 birth time, timestamp
        public BigInteger MotherId; //uint256 mother id, initial 0
        public BigInteger FatherId; //uint256 father id, the initial generation is 0
        public BigInteger Generation; //uint 256 generation

        public BigInteger Name;
        public string ImageUrl;
        public int IsBrawling;
        public int IsBreeding;

        public BigInteger Health;
        public BigInteger Mana;
        public BigInteger Agility;
        public BigInteger Stamina;
        public BigInteger CriticalStrike;
        public BigInteger AttackSpeed;
        public BigInteger Versatility;
    }
}
