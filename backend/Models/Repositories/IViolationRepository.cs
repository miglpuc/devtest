using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models.Entities;

namespace backend.Models.Repositories
{
    public interface IViolationRepository
    {
        Task<List<Violation>> GetViolationListAsync();
        Task<List<ViolationType>> GetViolationTypeListAsync();

        Task<Violation> GetViolationDetailAsync(int id);
        Task<ViolationType> GetViolationTypeDetailAsync(int id);

        Task<bool> AddAsync(Violation violation);
        Task<bool> AddAsync(ViolationType violation);

        Task<bool> UpdateAsync(int id, Violation data);
        Task<bool> UpdateAsync(int id, ViolationType data);

        Task<bool> RemoveAsync(int id);
        Task<bool> RemoveViolationTypeAsync(int id);
    }
}
