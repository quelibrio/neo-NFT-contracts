using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeoNftProject.Models
{
	public class CreateTokenInputModel
	{
		public string Nickname { get; set; }

		public string TxId { get; set; }

		public int Health { get; set; }

		public int Mana { get; set; }

		public int Agility { get; set; }

		public int Stamina { get; set; }

		public int CriticalStrike { get; set; }

		public int AttackSpeed { get; set; }

		public int Experience { get; set; }

		public int Level { get; set; }

		public bool IsBreeding { get; set; }

		public bool IsBrawling { get; set; }

		public int Mastery { get; set; }

		public int Versatility { get; set; }

		public string ImageUrl { get; set; }

		public int AddressId { get; set; }
	}
}
