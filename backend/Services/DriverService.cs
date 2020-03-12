using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models.Entities;
using backend.Models.Repositories;
using backend.Models.Services;

namespace backend.Services
{
    public class DriverService: IDriverService
    {
        private readonly IDriverRepository _driverRepository;
        private readonly IVehicleRepository _vehicleRepository;

        public DriverService(IDriverRepository driverRepository, IVehicleRepository vehicleRepository)
        {
            _driverRepository = driverRepository;
            _vehicleRepository = vehicleRepository;
        }

        /// <summary>
        /// Business logic for getting list of drivers
        /// </summary>
        /// <returns>List or null</returns>
        public async Task<List<DriverResponse>> GetDriverListAsync()
        {
            return await _driverRepository.GetListAsync();
        }

        /// <summary>
        /// Business logic for getting single driver
        /// </summary>
        /// <param name="id">Driver id</param>
        /// <returns>Detail or null</returns>
        public async Task<DriverResponse> GetDriverDetailAsync(int id)
        {
            return await _driverRepository.GetDetailAsync(d => d.DriverId == id);
        }

        /// <summary>
        /// Business logic for adding new driver
        /// </summary>
        /// <param name="driver">Driver data</param>
        /// <returns>True or false</returns>
        public async Task<bool> AddDriverAsync(DriverRequest data)
        {
            // Check if driver exists by DNI before adding
            var driver = await _driverRepository.GetDetailAsync(d => d.Dni.Equals(data.Dni));

            // If driver doesn´t exist, so add it
            if (driver == null)
            {
                return await _driverRepository.AddAsync(data);
            }

            return false;
        }

        /// <summary>
        /// Business logic for updating driver
        /// </summary>
        /// <param name="id">Driver id</param>
        /// <param name="driver">Driver data</param>
        /// <returns></returns>
        public async Task<bool> UpdateDriverAsync(int id, DriverRequest data)
        {
            return await _driverRepository.UpdateAsync(id, data);
        }

        /// <summary>
        /// Business logic for removing driver
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> RemoveDriverAsync(int id)
        {
            return await _driverRepository.RemoveAsync(id);
        }
    }
}
