using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models.Entities;

namespace backend.Models.Services
{
    public interface IViolationService
    {
        Task<List<Violation>> GetViolationListAsync();
        Task<List<ViolationType>> GetViolationTypeListAsync();

        Task<Violation> GetViolationDetailAsync(int id);
        Task<ViolationType> GetViolationTypeDetailAsync(int id);

        Task<bool> AddViolationAsync(Violation violation);
        Task<bool> AddViolationTypeAsync(ViolationType violationType);

        Task<bool> UpdateViolationAsync(int id, Violation data);
        Task<bool> UpdateViolationTypeAsync(int id, ViolationType data);

        Task<bool> RemoveViolationAsync(int id);
        Task<bool> RemoveViolationTypeAsync(int id);
    }
}
