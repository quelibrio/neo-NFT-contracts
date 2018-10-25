using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NeoNftProject.Settings
{
	public class NeoSettings
	{
		public string DataDirectoryPath { get; private set; }
		public ushort NodePort { get; private set; }
		public ushort WsPort { get; private set; }
		public string[] UriPrefix { get; private set; }
		public string SslCert { get; private set; }
		public string SslCertPassword { get; private set; }

		public static NeoSettings Default { get; private set; }

		static NeoSettings()
		{
			IConfigurationSection section = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("config.json")
				.Build()
				.GetSection("ApplicationConfiguration");

			Default = new NeoSettings
			{
				DataDirectoryPath = section.GetSection("DataDirectoryPath").Value,
				NodePort = ushort.Parse(section.GetSection("NodePort").Value),
				WsPort = ushort.Parse(section.GetSection("WsPort").Value),
				UriPrefix = section.GetSection("UriPrefix").GetChildren().Select(p => p.Value).ToArray(),
				SslCert = section.GetSection("SslCert").Value,
				SslCertPassword = section.GetSection("SslCertPassword").Value
			};
		}
	}
}
