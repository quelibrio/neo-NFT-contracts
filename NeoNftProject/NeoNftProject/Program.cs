using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Neo;
using Neo.Persistence.LevelDB;
using NeoNftProject.Settings;

namespace NeoNftProject
{
	public class Program
	{
		public static NeoSystem NeoSystem { get; private set; }

		public static void Main(string[] args)
		{
			InitializeNeoSystem(args);
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>();

		private static void InitializeNeoSystem(string[] args)
		{
			LevelDBStore store = new LevelDBStore(NeoSettings.Default.DataDirectoryPath);
			NeoSystem = new NeoSystem(store);

			NeoSystem.StartNode(NeoSettings.Default.NodePort, NeoSettings.Default.WsPort);
			CreateWebHostBuilder(args).Build().Run();
		}
	}
}
