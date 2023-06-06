using AggregationApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AggregationApp.Data
{

    public class ElectricityDbContext : DbContext
    {
        public ElectricityDbContext(DbContextOptions<ElectricityDbContext> options) : base(options)
        {
        }
        public ElectricityDbContext() : base()
        {
        }

        public DbSet<ElectricityDataModel> ElectricityData { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ElectricityDataModel>(entity =>
            { 
                entity.Property(e => e.PPlus)
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PMinus)
                    .HasColumnType("decimal(18, 2)");
            });
        }
        
    }
}

