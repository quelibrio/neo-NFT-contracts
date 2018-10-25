using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeoNftProject.Data
{
	public class NeoNftContext : DbContext
	{
		public NeoNftContext(DbContextOptions<NeoNftContext> options)
			: base(options)
		{
		}

		public DbSet<Token> Tokens { get; set; }
		public DbSet<Address> Addresses { get; set; }
		public DbSet<Transaction> Transactions { get; set; }
		public DbSet<Auction> Auctions { get; set; }


		protected override void OnModelCreating(ModelBuilder builder)
		{

			builder.Entity<Transaction>()
				.HasOne(c => c.Sender)
				.WithMany(c => c.OutgoingTransactions)
				.OnDelete(DeleteBehavior.Restrict);

			builder.Entity<Transaction>()
				.HasOne(c => c.Receiver)
				.WithMany(c => c.IncomingTransactions)
				.OnDelete(DeleteBehavior.Restrict);


			builder.Entity<Auction>()
				.HasOne(c=> c.Token)
				.WithMany(c => c.Auctions)
				.OnDelete(DeleteBehavior.Restrict);
		}

	}
}
