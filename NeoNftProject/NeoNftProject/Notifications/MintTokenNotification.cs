using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace NeoNftProject.Notifications
{
	public class MintTokenNotification
	{
        //byte[] owner, byte health,byte mana, byte agility,
        //byte stamina, byte criticalStrike, byte attackSpeed, byte versatility, byte mastery, BigInteger level

        public int TokenId { get; set; }
        public byte[] Owner { get; set; }
        public int Health { get; set; }
        public int Mana { get; set; }
        public int Agility { get; set; }
        public int Stamina { get; set; }
        public int CriticalStrike { get; set; }
        public int AttackSpeed { get; set; }
        public int Versatility { get; set; }
        public int Mastery { get; set; }
        public int Level { get; set; }
    }
}
