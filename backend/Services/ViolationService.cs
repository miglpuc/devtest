using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models.Entities;
using backend.Models.Repositories;
using backend.Models.Services;

namespace backend.Services
{
    public class ViolationService: IViolationService
    {
        private readonly IViolationRepository _violationRepository;

        public ViolationService(IViolationRepository violationRepository)
        {
            _violationRepository = violationRepository;
        }

        /// <summary>
        /// Business logic for getting list of violations
        /// </summary>
        /// <returns>List or null</returns>
        public async Task<List<Violation>> GetViolationListAsync()
        {
            return await _violationRepository.GetViolationListAsync();
        }

        /// <summary>
        /// Business logic for getting list of violation types
        /// </summary>
        /// <returns>List or null</returns>
        public async Task<List<ViolationType>> GetViolationTypeListAsync()
        {
            return await _violationRepository.GetViolationTypeListAsync();
        }

        /// <summary>
        /// Business logic for getting single violation
        /// </summary>
        /// <param name="id">Violation id</param>
        /// <returns>Detail or null</returns>
        public async Task<Violation> GetViolationDetailAsync(int id)
        {
            return await _violationRepository.GetViolationDetailAsync(id);
        }

        /// <summary>
        /// Business logic for getting single violation type
        /// </summary>
        /// <param name="id">Violation type id</param>
        /// <returns>Detail or null</returns>
        public async Task<ViolationType> GetViolationTypeDetailAsync(int id)
        {
            return await _violationRepository.GetViolationTypeDetailAsync(id);
        }

        /// <summary>
        /// Business logic for adding new violation
        /// </summary>
        /// <param name="violation">Violation data</param>
        /// <returns>True or false</returns>
        public async Task<bool> AddViolationAsync(Violation violation)
        {
            return await _violationRepository.AddAsync(violation);
        }

        /// <summary>
        /// @overloading
        /// Business logic for adding new violation type
        /// </summary>
        /// <param name="violation">Violation type data</param>
        /// <returns>True or false</returns>
        public async Task<bool> AddViolationTypeAsync(ViolationType violationType)
        {
            return await _violationRepository.AddAsync(violationType);
        }

        /// <summary>
        /// Business logic for updating violation
        /// </summary>
        /// <param name="id">Violation id</param>
        /// <param name="violation">Violation data</param>
        /// <returns></returns>
        public async Task<bool> UpdateViolationAsync(int id, Violation data)
        {
            return await _violationRepository.UpdateAsync(id, data);
        }

        /// <summary>
        /// @overloading
        /// Business logic for updating violation type
        /// </summary>
        /// <param name="id">Violation type id</param>
        /// <param name="violation">Violation type data</param>
        /// <returns></returns>
        public async Task<bool> UpdateViolationTypeAsync(int id, ViolationType data)
        {
            return await _violationRepository.UpdateAsync(id, data);
        }

        /// <summary>
        /// Business logic for removing violation
        /// </summary>
        /// <param name="id">Violation id</param>
        /// <returns></returns>
        public async Task<bool> RemoveViolationAsync(int id)
        {
            return await _violationRepository.RemoveAsync(id);
        }

        /// <summary>
        /// Business logic for removing violation type
        /// </summary>
        /// <param name="id">Violation type id</param>
        /// <returns></returns>
        public async Task<bool> RemoveViolationTypeAsync(int id)
        {
            return await _violationRepository.RemoveViolationTypeAsync(id);
        }
    }
}
