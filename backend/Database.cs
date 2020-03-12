using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using backend.Models.Entities;

namespace backend.Database
{
    public class DatabaseContext: DbContext
    {
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Violation> Violations { get; set; }
        public DbSet<ViolationType> ViolationsType { get; set; }
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DriverVehicle>()
                .HasKey(data => new { data.DriverId, data.VehicleId });

            modelBuilder.Entity<DriverVehicle>()
                .HasOne(data => data.Driver)
                .WithMany(data => data.DriverVehicles)
                .HasForeignKey(data => data.DriverId);

            modelBuilder.Entity<DriverVehicle>()
                .HasOne(data => data.Vehicle)
                .WithMany(data => data.DriverVehicles)
                .HasForeignKey(data => data.VehicleId);
        }
    }
}
