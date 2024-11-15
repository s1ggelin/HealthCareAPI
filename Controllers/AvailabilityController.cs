using HealthCareABApi.Models;
using HealthCareABApi.Repositories;
using HealthCareABApi.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthCareABApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvailabilityController : ControllerBase
    {
        private readonly IAvailabilityRepository _availabilityRepository;

        public AvailabilityController(IAvailabilityRepository availabilityRepository)
        {
            _availabilityRepository = availabilityRepository;
        }

        [HttpPost]
        public async Task<IActionResult> AddAvailability([FromBody] Availability availability)
        {
            if (availability == null || availability.AvailableSlots == null || !availability.AvailableSlots.Any())
                return BadRequest("Invalid availability data.");

            await _availabilityRepository.AddAvailabilityAsync(availability);
            return Ok("Availability added successfully.");
        }

        [HttpGet("{caregiverId}")]
        public async Task<IActionResult> GetAvailabilities(int caregiverId)
        {
            var availabilities = await _availabilityRepository.GetAvailabilitiesAsync(caregiverId);
            return Ok(availabilities);
        }

    }
}
