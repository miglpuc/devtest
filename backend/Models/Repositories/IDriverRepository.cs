using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using backend.Models.Entities;

namespace backend.Models.Repositories
{
    public interface IDriverRepository
    {
        Task<List<DriverResponse>> GetListAsync();
        Task<DriverResponse> GetDetailAsync(Expression<Func<DriverResponse, bool>> query);
        Task<bool> AddAsync(DriverRequest data);
        Task<bool> UpdateAsync(int id, DriverRequest data);
        Task<bool> RemoveAsync(int id);
    }
}
