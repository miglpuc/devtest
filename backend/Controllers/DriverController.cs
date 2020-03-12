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
    public class DriverController: ControllerBase
    {
        private readonly IDriverService _driverService;

        public DriverController(IDriverService driverService)
        {
            _driverService = driverService;
        }

        /// <summary>
        /// Gets list of drivers
        /// </summary>
        /// <returns>List of driver</returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _driverService.GetDriverListAsync();

            if (response != null)
            {
                return Ok(response);
            }

            return NotFound(null);
        }

        /// <summary>
        /// Gets single driver
        /// </summary>
        /// <param name="id">Driver id</param>
        /// <returns>Detail of driver</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {

            var response = await _driverService.GetDriverDetailAsync(id);

            if (response != null)
            {
                return Ok(response);
            }

            return NotFound(null);
        }

        /// <summary>
        /// Adds new driver
        /// </summary>
        /// <param name="request">Driver data</param>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DriverRequest data)
        {

            var response = await _driverService.AddDriverAsync(data);

            if (response)
            {
                return Ok(response);
            }

            return NotFound(null);
        }

        /// <summary>
        /// Updates driver
        /// </summary>
        /// <param name="id">Driver id</param>
        /// <param name="data">Driver data</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] DriverRequest data)
        {
            var response = await _driverService.UpdateDriverAsync(id, data);

            if (response)
            {
                return Ok(response);
            }

            return NotFound(null);
        }

        /// <summary>
        /// Removes driver
        /// </summary>
        /// <param name="id">Driver id</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _driverService.RemoveDriverAsync(id);

            if (response)
            {
                return Ok(response);
            }

            return NotFound(null);
        }
    }
}
