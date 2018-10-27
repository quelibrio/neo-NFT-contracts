using System.Numerics;

namespace NeoNftImplementation.NftContract.Models
{
    /// <summary>
    /// Skills and weapon configuration
    /// </summary>
    public class AttributeConfig
    {
        // General skills
        public BigInteger NormalSkillIdMin; // id minimum
        public BigInteger NormalSkillIdMax; // id max

        // Rare skills
        public BigInteger RareSkillIdMin;
        public BigInteger RareSkillIdMax;

        // General equipment
        public BigInteger NormalEquipIdMin;
        public BigInteger NormalEquipIdMax;

        // Rare equipment
        public BigInteger RareEquipIdMax;
        public BigInteger RareEquipIdMin;

        // Appearance attribute maximum
        public BigInteger Atr1Max;
        public BigInteger Atr2Max;
        public BigInteger Atr3Max;
        public BigInteger Atr4Max;
        public BigInteger Atr5Max;
        public BigInteger Atr6Max;
        public BigInteger Atr7Max;
        public BigInteger Atr8Max;
        public BigInteger Atr9Max;
    }
}
