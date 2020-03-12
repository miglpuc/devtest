using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using backend.Models.Entities;

namespace backend.Models.Repositories
{
    public interface IVehicleRepository
    {
        Task<List<VehicleResponse>> GetListAsync();
        Task<VehicleResponse> GetDetailAsync(Expression<Func<VehicleResponse, bool>> query);
        Task<bool> AddAsync(VehicleRequest data);
        Task<bool> UpdateAsync(int id, VehicleRequest data);
        Task<bool> RemoveAsync(int id);
    }
}
