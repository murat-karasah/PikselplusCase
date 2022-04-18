using Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class PikselDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=DESKTOP-28ABEQE;Database=myExampleDb;Trusted_Connection = true;");

        }
        public DbSet<UserEntity> UserEntities{ get; set; }
        public DbSet<InvoiceEntity> InvoiceEntities{ get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InvoiceEntity>().HasKey(e => e.Id);
             modelBuilder.Entity<UserEntity>().HasKey(e => e.Id);
            modelBuilder.Entity<InvoiceEntity>()
                            .HasOne(i => i.user)
                            .WithMany(s => s.ınvoiceEntities)
                        .HasForeignKey(i => i.UserId);

        }
    }
}
