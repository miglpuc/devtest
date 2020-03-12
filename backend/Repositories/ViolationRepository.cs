using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using backend.Database;
using backend.Models.Entities;
using backend.Models.Repositories;

namespace backend.Repositories
{
    public class ViolationRepository: IViolationRepository
    {
        private readonly DatabaseContext _db;

        public ViolationRepository(DatabaseContext databaseContext)
        {
            _db = databaseContext;
        }

        /// <summary>
        /// Gets list of violations from database table
        /// </summary>
        /// <returns>List or null</returns>
        public async Task<List<Violation>> GetViolationListAsync()
        {
            return await _db.Violations
                .Include(v => v.Driver)
                .Include(v => v.Vehicle)
                .Include(v => v.ViolationType)
                .ToListAsync();
        }

        /// <summary>
        /// Gets list of violation types from database table
        /// </summary>
        /// <returns>List or null</returns>
        public async Task<List<ViolationType>> GetViolationTypeListAsync()
        {
            return await _db.ViolationsType.ToListAsync();
        }

        /// <summary>
        /// Gets detail of violation from database table by id
        /// </summary>
        /// <param name="id">Violation id</param>
        /// <returns>Detail or null</returns>
        public async Task<Violation> GetViolationDetailAsync(int id)
        {
            return await _db.Violations
                .Include(v => v.Driver)
                .Include(v => v.Vehicle)
                .Include(v => v.ViolationType)
                .SingleOrDefaultAsync(v => v.ViolationId == id);
        }

        /// <summary>
        /// Gets detail of violation type from database table by id
        /// </summary>
        /// <param name="id">Violation id</param>
        /// <returns>Detail or null</returns>
        public async Task<ViolationType> GetViolationTypeDetailAsync(int id)
        {
            return await _db.ViolationsType.SingleOrDefaultAsync(vt => vt.ViolationTypeId == id);
        }

        /// <summary>
        /// Adds violation into database table
        /// </summary>
        /// <param name="violation">Violation data</param>
        /// <returns>True or false</returns>
        public async Task<bool> AddAsync(Violation violation)
        {
            var driver = _db.Drivers.Single(d => d.Dni.Equals(violation.Driver.Dni));
            var vehicle = _db.Vehicles.Single(v => v.Plate.Equals(violation.Vehicle.Plate));
            var violationType = _db.ViolationsType.Single(vt => vt.ViolationTypeId == violation.ViolationType.ViolationTypeId);

            // Substract of points before adding in relation to the violation type
            driver.Points -= violationType.Points;

            // Creates violation
            violation.Driver = driver;
            violation.Vehicle = vehicle;
            violation.ViolationType = violationType;
            violation.CreatedOn = DateTime.Now;
            violation.UpdatedOn = DateTime.Now;

            _db.Violations.Add(violation);

            return (await _db.SaveChangesAsync()) > 0;
        }

        /// <summary>
        /// @overloading
        /// Adds violation type into database table
        /// </summary>
        /// <param name="violation">Violation type data</param>
        /// <returns>True or false</returns>
        public async Task<bool> AddAsync(ViolationType violationType)
        {
            violationType.CreatedOn = DateTime.Now;
            violationType.UpdatedOn = DateTime.Now;

            _db.ViolationsType.Add(violationType);

            return (await _db.SaveChangesAsync()) > 0;
        }

        /// <summary>
        /// Updates violation from database table
        /// </summary>
        /// <param name="id">Violation id</param>
        /// <param name="violation">Violation data</param>
        /// <returns>True or false</returns>
        public async Task<bool> UpdateAsync(int id, Violation data)
        {
            var violation = _db.Violations
                .Include(v => v.ViolationType)
                .Single(v => v.ViolationId == id);

            var driver = _db.Drivers.Single(d => d.Dni.Equals(data.Driver.Dni));
            var vehicle = _db.Vehicles.Single(v => v.Plate.Equals(data.Vehicle.Plate));
            var violationType = _db.ViolationsType.Single(vt => vt.ViolationTypeId == data.ViolationType.ViolationTypeId);

            // Addition of points before updating for restore the original value
            driver.Points += violation.ViolationType.Points;

            // Substract of points before updating violation in relation to the new violation type (in case of)
            driver.Points -= violationType.Points;

            violation.Driver = driver;
            violation.Vehicle = vehicle;
            violation.ViolationType = violationType;
            violation.UpdatedOn = DateTime.Now;

            return (await _db.SaveChangesAsync()) > 0;
        }

        /// <summary>
        /// @overloading
        /// Updates violation type from database table
        /// </summary>
        /// <param name="id">Violation type id</param>
        /// <param name="violation">Violation type data</param>
        /// <returns>True or false</returns>
        public async Task<bool> UpdateAsync(int id, ViolationType data)
        {
            var violationType = _db.ViolationsType.Single(vt => vt.ViolationTypeId == id);

            violationType.Description = data.Description;
            violationType.Points = data.Points;
            violationType.UpdatedOn = DateTime.Now;

            return (await _db.SaveChangesAsync()) > 0;
        }

        /// <summary>
        /// Removes violation from database table
        /// </summary>
        /// <param name="id">Violation id</param>
        /// <returns>True or false</returns>
        public async Task<bool> RemoveAsync(int id)
        {
            var violation = _db.Violations.Single(v => v.ViolationId == id);

            _db.Violations.Remove(violation);

            return (await _db.SaveChangesAsync()) > 0;
        }

        /// <summary>
        /// Removes violation type from database table
        /// </summary>
        /// <param name="id">Violation id</param>
        /// <returns>True or false</returns>
        public async Task<bool> RemoveViolationTypeAsync(int id)
        {
            var violationType = _db.ViolationsType.Single(vt => vt.ViolationTypeId == id);

            _db.ViolationsType.Remove(violationType);

            return (await _db.SaveChangesAsync()) > 0;
        }
    }
}
