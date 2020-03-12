using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using backend.Models.Entities;
using backend.Models.Services;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViolationController: ControllerBase
    {
        private readonly IViolationService _violationService;

        public ViolationController(IViolationService violationService)
        {
            _violationService = violationService;
        }

        /// <summary>
        /// Gets list of violations
        /// </summary>
        /// <returns>List of violations</returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _violationService.GetViolationListAsync();

            if (response != null)
            {
                return Ok(response);
            }

            return NotFound(null);
        }

        /// <summary>
        /// Gets list of violation types
        /// </summary>
        /// <returns>List of violation types</returns>
        [HttpGet("types")]
        public async Task<IActionResult> GetTypes()
        {
            var response = await _violationService.GetViolationTypeListAsync();

            if (response != null)
            {
                return Ok(response);
            }

            return NotFound(null);
        }

        /// <summary>
        /// Gets single violation
        /// </summary>
        /// <param name="id">Violation id</param>
        /// <returns>Detail of violation</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {

            var response = await _violationService.GetViolationDetailAsync(id);

            if (response != null)
            {
                return Ok(response);
            }

            return NotFound(null);
        }

        /// <summary>
        /// Gets single violation type
        /// </summary>
        /// <param name="id">Violation type id</param>
        /// <returns>Detail of violation type</returns>
        [HttpGet("types/{id}")]
        public async Task<IActionResult> GetType(int id)
        {

            var response = await _violationService.GetViolationTypeDetailAsync(id);

            if (response != null)
            {
                return Ok(response);
            }

            return NotFound(null);
        }

        /// <summary>
        /// Adds new violation
        /// </summary>
        /// <param name="request">Violation data</param>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Violation data)
        {

            var response = await _violationService.AddViolationAsync(data);

            if (response)
            {
                return Ok(response);
            }

            return NotFound(null);
        }

        /// <summary>
        /// Adds new violation type
        /// </summary>
        /// <param name="request">Violation type data</param>
        [HttpPost("types")]
        public async Task<IActionResult> PostType([FromBody] ViolationType data)
        {

            var response = await _violationService.AddViolationTypeAsync(data);

            if (response)
            {
                return Ok(response);
            }

            return NotFound(null);
        }

        /// <summary>
        /// Updates violation
        /// </summary>
        /// <param name="id">Violation id</param>
        /// <param name="data">Violation data</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Violation data)
        {
            var response = await _violationService.UpdateViolationAsync(id, data);

            if (response)
            {
                return Ok(response);
            }

            return NotFound(null);
        }

        /// <summary>
        /// Updates violation type
        /// </summary>
        /// <param name="id">Violation type id</param>
        /// <param name="data">Violation type data</param>
        [HttpPut("types/{id}")]
        public async Task<IActionResult> PutType(int id, [FromBody] ViolationType data)
        {
            var response = await _violationService.UpdateViolationTypeAsync(id, data);

            if (response)
            {
                return Ok(response);
            }

            return NotFound(null);
        }

        /// <summary>
        /// Removes violation
        /// </summary>
        /// <param name="id">Violation id</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _violationService.RemoveViolationAsync(id);

            if (response)
            {
                return Ok(response);
            }

            return NotFound(null);
        }

        /// <summary>
        /// Removes violation type
        /// </summary>
        /// <param name="id">Violation type id</param>
        [HttpDelete("types/{id}")]
        public async Task<IActionResult> DeleteType(int id)
        {
            var response = await _violationService.RemoveViolationTypeAsync(id);

            if (response)
            {
                return Ok(response);
            }

            return NotFound(null);
        }
    }
}
