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
    public class VehicleController: ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        /// <summary>
        /// Gets list of vehicles
        /// </summary>
        /// <returns>List of vehicle</returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _vehicleService.GetVehicleListAsync();

            if (response != null)
            {
                return Ok(response);
            }

            return NotFound(null);
        }

        /// <summary>
        /// Gets single vehicle
        /// </summary>
        /// <param name="id">Vehicle id</param>
        /// <returns>Detail of vehicle</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {

            var response = await _vehicleService.GetVehicleDetailAsync(id);

            if (response != null)
            {
                return Ok(response);
            }

            return NotFound(null);
        }

        /// <summary>
        /// Adds new vehicle
        /// </summary>
        /// <param name="request">Vehicle data</param>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] VehicleRequest data)
        {

            var response = await _vehicleService.AddVehicleAsync(data);

            if (response)
            {
                return Ok(response);
            }

            return NotFound(null);
        }

        /// <summary>
        /// Updates vehicle
        /// </summary>
        /// <param name="id">Vehicle id</param>
        /// <param name="data">Vehicle data</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] VehicleRequest data)
        {
            var response = await _vehicleService.UpdateVehicleAsync(id, data);

            if (response)
            {
                return Ok(response);
            }

            return NotFound(null);
        }

        /// <summary>
        /// Removes vehicle
        /// </summary>
        /// <param name="id">Vehicle id</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _vehicleService.RemoveVehicleAsync(id);

            if (response)
            {
                return Ok(response);
            }

            return NotFound(null);
        }
    }
}
