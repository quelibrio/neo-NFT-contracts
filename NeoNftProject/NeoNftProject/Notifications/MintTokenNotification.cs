using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace NeoNftProject.Notifications
{
	public class MintTokenNotification
	{
        //BigInteger tokenId, byte[] owner, BigInteger agility, BigInteger attackSpeed, BigInteger criticalStrike, BigInteger nextActionAt,
        //    BigInteger cloneWithId, BigInteger birthTime, BigInteger matronId, BigInteger sireId, BigInteger generation

        public BigInteger TokenId { get; set; }
        public byte[] Owner { get; set; }
        public BigInteger Agility { get; set; }
        public BigInteger AttackSpeed { get; set; }
        public BigInteger CriticalStrike { get; set; }
        public BigInteger NextActionAt { get; set; }
        public BigInteger CloneWithId { get; set; }
        public BigInteger BirthTime { get; set; }
        public BigInteger MatronId { get; set; }
        public BigInteger SireId { get; set; }
        public BigInteger Generation { get; set; }


    }
}
