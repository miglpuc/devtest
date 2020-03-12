using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models.Entities;

namespace backend.Models.Services
{
    public interface IVehicleService
    {
        Task<List<VehicleResponse>> GetVehicleListAsync();
        Task<VehicleResponse> GetVehicleDetailAsync(int id);
        Task<bool> AddVehicleAsync(VehicleRequest data);
        Task<bool> UpdateVehicleAsync(int id, VehicleRequest data);
        Task<bool> RemoveVehicleAsync(int id);
    }
}
