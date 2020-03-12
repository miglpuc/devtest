using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models.Entities;

namespace backend.Models.Services
{
    public interface IDriverService
    {
        Task<List<DriverResponse>> GetDriverListAsync();
        Task<DriverResponse> GetDriverDetailAsync(int id);
        Task<bool> AddDriverAsync(DriverRequest data);
        Task<bool> UpdateDriverAsync(int id, DriverRequest data);
        Task<bool> RemoveDriverAsync(int id);
    }
}
