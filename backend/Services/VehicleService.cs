using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using backend.Models.Entities;
using backend.Models.Repositories;
using backend.Models.Services;

namespace backend.Services
{
    public class VehicleService: IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IDriverRepository _driverRepository;

        public VehicleService(IVehicleRepository vehicleRepository, IDriverRepository driverRepository)
        {
            _vehicleRepository = vehicleRepository;
            _driverRepository = driverRepository;
        }

        /// <summary>
        /// Business logic for getting list of vehicles
        /// </summary>
        /// <returns>List or null</returns>
        public async Task<List<VehicleResponse>> GetVehicleListAsync()
        {
            return await _vehicleRepository.GetListAsync();
        }

        /// <summary>
        /// Business logic for getting single vehicle
        /// </summary>
        /// <param name="id">vehicle id</param>
        /// <returns>Detail or null</returns>
        public async Task<VehicleResponse> GetVehicleDetailAsync(int id)
        {
            return await _vehicleRepository.GetDetailAsync(v => v.VehicleId == id);
        }

        /// <summary>
        /// Business logic for adding new vehicle
        /// </summary>
        /// <param name="vehicle">vehicle data</param>
        /// <returns>True or false</returns>
        public async Task<bool> AddVehicleAsync(VehicleRequest data)
        {
            // Check if vehicle exists by plate before adding
            var vehicle = await _vehicleRepository.GetDetailAsync(v => v.Plate.Equals(data.Plate));

            // Check if driver exists by DNI before adding
            var driver = await _driverRepository.GetDetailAsync(d => d.Dni.Equals(data.Driver.Dni));

            // If vehicle exists, or driver doesn't exist or driver has more than 10 vehicles, return error
            if (vehicle != null || driver == null || driver.Vehicles.Count > 10)
            {
                return false;
            }

            return await _vehicleRepository.AddAsync(data);
        }

        /// <summary>
        /// Business logic for updating vehicle
        /// </summary>
        /// <param name="id">vehicle id</param>
        /// <param name="vehicle">vehicle data</param>
        /// <returns></returns>
        public async Task<bool> UpdateVehicleAsync(int id, VehicleRequest data)
        {
            // If driver data is included, so checking before updating
            if (data.Driver != null)
            {
                var driver = await _driverRepository.GetDetailAsync(d => d.Dni.Equals(data.Driver.Dni));

                // If driver doesn't exist or driver has more than 10 vehicles, return error
                if (driver == null || driver.Vehicles.Count > 10)
                {
                    return false;
                }
            }

            return await _vehicleRepository.UpdateAsync(id, data);
        }

        /// <summary>
        /// Business logic for removing vehicle
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> RemoveVehicleAsync(int id)
        {
            return await _vehicleRepository.RemoveAsync(id);
        }
    }
}
