﻿using Entities;
using System.Data.Entity;

namespace Data
{
    public class DataContext : DbContext
    {
        public DbSet<BookEM> Books { get; set; }
        public DbSet<AuthorEM> Authors { get; set; }

        static DataContext()
        {
            Database.SetInitializer(new DataContextInitializer());
        }

        public DataContext() : base("name=DataContext")
        {
            var ensureDllIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookEM>().HasKey(k => k.Id);
            modelBuilder.Entity<AuthorEM>().HasKey(k => k.Id);
            modelBuilder.Entity<AuthorEM>().HasMany(a => a.Books).WithMany(b => b.Authors);
        }
    }
}