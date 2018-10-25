using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace NeoNftProject.Notifications
{
	public class TransferNotification
	{

		public byte[] From { get; set; }

		public byte[] To { get; set; }

		public BigInteger Value { get; set; }
	}
}
