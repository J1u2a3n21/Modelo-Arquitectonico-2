using McNutsAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace McNutsAPI.Data
{
    public class PeanutDbContext:DbContext
    {
        public DbSet<PeanutEntity> Peanuts { get; set; }
        public DbSet<ClientEntity> Clients { get; set; }
        public PeanutDbContext(DbContextOptions<PeanutDbContext> options) : base(options){}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PeanutEntity>().ToTable("Peanut");
            modelBuilder.Entity<PeanutEntity>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<PeanutEntity>().HasMany(p => p.Clients).WithOne(c => c.Peanut);

            modelBuilder.Entity<ClientEntity>().ToTable("Client");
            modelBuilder.Entity<ClientEntity>().Property(c => c.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<ClientEntity>().HasOne(c => c.Peanut).WithMany(p => p.Clients);

            //dotnet tool install --global dotnet-ef
            //dotnet ef migrations add InitialCreate
            //dotnet ef database update
        }
    }
}
