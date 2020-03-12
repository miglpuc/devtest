using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using backend.Database;
using backend.Models.Entities;
using backend.Models.Repositories;

namespace backend.Repositories
{
    public class VehicleRepository: IVehicleRepository
    {
        private readonly DatabaseContext _db;

        public VehicleRepository(DatabaseContext databaseContext)
        {
            _db = databaseContext;
        }

        /// <summary>
        /// Gets list of vehicles from database table
        /// </summary>
        /// <returns>List or null</returns>
        public async Task<List<VehicleResponse>> GetListAsync()
        {
            var result = await _db.Vehicles
                .Include(v => v.DriverVehicles)
                .ThenInclude(dv => dv.Driver)
                .Select(v => new VehicleResponse {
                    VehicleId = v.VehicleId,
                    Plate = v.Plate,
                    Brand = v.Brand,
                    Model = v.Model,
                    CreatedOn = v.CreatedOn,
                    UpdatedOn = v.UpdatedOn,
                    Drivers = v.DriverVehicles.Select(dv => dv.Driver).ToList()
                })
                .ToListAsync();

            return result;
        }

        /// <summary>
        /// Gets detail of vehicle from database table by expression provided
        /// </summary>
        /// <param name="query">Expression</param>
        /// <returns>Detail or null</returns>
        public async Task<VehicleResponse> GetDetailAsync(Expression<Func<VehicleResponse, bool>> query)
        {
            var result = await _db.Vehicles
                .Include(v => v.DriverVehicles)
                .ThenInclude(dv => dv.Driver)
                .Select(v => new VehicleResponse
                {
                    VehicleId = v.VehicleId,
                    Plate = v.Plate,
                    Brand = v.Brand,
                    Model = v.Model,
                    CreatedOn = v.CreatedOn,
                    UpdatedOn = v.UpdatedOn,
                    Drivers = v.DriverVehicles.Select(dv => dv.Driver).ToList()
                })
                .SingleOrDefaultAsync(query);

            return result;
        }

        /// <summary>
        /// Adds vehicle into database table
        /// </summary>
        /// <param name="vehicle">Vehicle data</param>
        /// <returns>True or false</returns>
        public async Task<bool> AddAsync(VehicleRequest data)
        {
            // First, creating vehicle
            var vehicle = new Vehicle
            {
                VehicleId = data.VehicleId,
                Plate = data.Plate,
                Brand = data.Brand,
                Model = data.Model,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now
            };

            // Second, adding driver
            var driver = _db.Drivers.Single(d => d.Dni.Equals(data.Driver.Dni));

            vehicle.DriverVehicles = new List<DriverVehicle>
            {
                new DriverVehicle
                {
                    Driver = driver,
                    Vehicle = vehicle
                }
            };

            _db.Vehicles.Add(vehicle);

            return (await _db.SaveChangesAsync()) > 0;
        }


        /// <summary>
        /// Updates vehicle from database table
        /// </summary>
        /// <param name="id">Vehicle id</param>
        /// <param name="vehicle">Vehicle data</param>
        /// <returns>True or false</returns>
        public async Task<bool> UpdateAsync(int id, VehicleRequest data)
        {
            // First, updating only vehicle data
            var vehicle = _db.Vehicles.Single(v => v.VehicleId == id);

            vehicle.Plate = data.Plate;
            vehicle.Brand = data.Brand;
            vehicle.Model = data.Model;
            vehicle.UpdatedOn = DateTime.Now;

            // Second, if driver data is included, so adding driver (optional)
            if (data.Driver != null)
            {
                var driver = _db.Drivers.Single(d => d.Dni.Equals(data.Driver.Dni));

                vehicle.DriverVehicles = new List<DriverVehicle>
                {
                    new DriverVehicle
                    {
                        Driver = driver,
                        Vehicle = vehicle
                    }
                };
            }

            return (await _db.SaveChangesAsync()) > 0;
        }

        /// <summary>
        /// Removes vehicle from database table
        /// </summary>
        /// <param name="id">Vehicle id</param>
        /// <returns>True or false</returns>
        public async Task<bool> RemoveAsync(int id)
        {
            var vehicle = _db.Vehicles.Single(v => v.VehicleId == id);

            _db.Vehicles.Remove(vehicle);

            return (await _db.SaveChangesAsync()) > 0;
        }
    }
}
