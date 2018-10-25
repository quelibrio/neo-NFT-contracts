using Akka.Actor;
using Microsoft.EntityFrameworkCore;
using NeoNftProject.Data;
using NeoNftProject.Extensions;
using NeoNftProject.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Neo.Ledger.Blockchain;

namespace NeoNftProject.Actors
{
	public class NotificationsReader : UntypedActor
	{
		private readonly string connectionString;
		


		public NotificationsReader(IActorRef blockchain, string connectionString)
		{
			blockchain.Tell(new Register());
			this.connectionString = connectionString;
		}

		protected override void OnReceive(object message)
		{
			if (message is ApplicationExecuted m)
			{
				var optionsBuilder = new DbContextOptionsBuilder<NeoNftContext>();
				optionsBuilder.UseSqlServer(connectionString);
				var db = new NeoNftContext(optionsBuilder.Options);

				foreach (var result in m.ExecutionResults)
				{
					foreach (var notification in result.Notifications)
					{
						var type = notification.GetNotificationType();
						if (type == "transfer")
						{
							var a = notification.GetNotification<TransferNotification>();
							//TODO save to database
						}

					}
				}

				db.Dispose();
			}
		}

		public static Props Props(IActorRef blockchain, string connectionString)
		{

			return Akka.Actor.Props.Create(() => new NotificationsReader(blockchain, connectionString));
		}
	}
}
