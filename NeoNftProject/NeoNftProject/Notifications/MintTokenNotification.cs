using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace NeoNftProject.Notifications
{
	public class MintTokenNotification
	{
		public byte[] Owner { get; set; }

		public byte Stregth { get; set; }

		public byte Power { get; set; }

		public byte Agile { get; set; }

		public byte Speed { get; set; }

		public BigInteger Generation { get; set; }
	}
}
