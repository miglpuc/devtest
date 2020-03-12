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
    public class DriverRepository: IDriverRepository
    {
        private readonly DatabaseContext _db;

        public DriverRepository(DatabaseContext databaseContext)
        {
            _db = databaseContext;
        }

        /// <summary>
        /// Gets list of drivers from database table
        /// </summary>
        /// <returns>List or null</returns>
        public async Task<List<DriverResponse>> GetListAsync()
        {
            return await _db.Drivers
                .Include(d => d.DriverVehicles)
                .ThenInclude(dv => dv.Vehicle)
                .Select(d => new DriverResponse {
                    DriverId = d.DriverId,
                    Dni = d.Dni,
                    Name = d.Name,
                    LastName = d.LastName,
                    Points = d.Points,
                    CreatedOn = d.CreatedOn,
                    UpdatedOn = d.UpdatedOn,
                    Vehicles = d.DriverVehicles.Select(dv => dv.Vehicle).ToList()
                })
                .ToListAsync();
        }

        /// <summary>
        /// Gets detail of driver from database table by expression provided
        /// </summary>
        /// <param name="id">Expression</param>
        /// <returns>Detail or null</returns>
        public async Task<DriverResponse> GetDetailAsync(Expression<Func<DriverResponse, bool>> query)
        {
            var result = await _db.Drivers
                .Include(v => v.DriverVehicles)
                .ThenInclude(dv => dv.Driver)
                .Select(d => new DriverResponse
                {
                    DriverId = d.DriverId,
                    Dni = d.Dni,
                    Name = d.Name,
                    LastName = d.LastName,
                    Points = d.Points,
                    CreatedOn = d.CreatedOn,
                    UpdatedOn = d.UpdatedOn,
                    Vehicles = d.DriverVehicles.Select(dv => dv.Vehicle).ToList()
                })
                .SingleOrDefaultAsync(query);

            return result;
        }

        /// <summary>
        /// Adds driver into database table
        /// </summary>
        /// <param name="driver">Driver data</param>
        /// <returns>True or false</returns>
        public async Task<bool> AddAsync(DriverRequest data)
        {
            var driver = new Driver
            {
                Dni = data.Dni,
                Name = data.Name,
                LastName = data.LastName,
                Points = data.Points,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now
            };

            _db.Drivers.Add(driver);

            return (await _db.SaveChangesAsync()) > 0;
        }

        /// <summary>
        /// Updates driver from database table
        /// </summary>
        /// <param name="id">Driver id</param>
        /// <param name="driver">Driver data</param>
        /// <returns>True or false</returns>
        public async Task<bool> UpdateAsync(int id, DriverRequest data)
        {
            var driver = _db.Drivers.Single(d => d.DriverId == id);

            driver.Dni = data.Dni;
            driver.Name = data.Name;
            driver.LastName = data.LastName;
            driver.Points = data.Points;
            driver.UpdatedOn = DateTime.Now;

            return (await _db.SaveChangesAsync()) > 0;
        }

        /// <summary>
        /// Removes driver from database table
        /// </summary>
        /// <param name="id">Driver id</param>
        /// <returns>True or false</returns>
        public async Task<bool> RemoveAsync(int id)
        {
            var driver = _db.Drivers.Single(d => d.DriverId == id);

            _db.Drivers.Remove(driver);

            return (await _db.SaveChangesAsync()) > 0;
        }
    }
}
