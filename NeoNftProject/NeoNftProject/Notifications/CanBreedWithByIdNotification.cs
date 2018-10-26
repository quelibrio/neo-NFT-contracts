using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace NeoNftProject.Notifications
{
	public class CanBreedWithByIdNotification
	{
		public BigInteger MotherId { get; set; }

		public BigInteger FatherId { get; set; }
	}
}
