using HealthCareABApi.Models;
using HealthCareABApi.Repositories;

using HealthCareABApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthCareABApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {

        private readonly AppointmentService _service;
        public AppointmentsController(AppointmentService service)
        {
            _service = service;
        }

        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentsController(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAppointment([FromBody] Appointment appointment)
        {
            if (appointment == null)
                return BadRequest("Invalid appointment data.");

            try
            {
                await _service.CreateAsync(appointment);
                return Ok("Appointment created succesfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAppointment(int id)
        {
            try
            {
            var appointment = await _service.GetByIdAsync(id);
            return Ok(appointment);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new {message = ex.Message });
            }
  
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAppointments()
        {
            var appointments = await _service.GetAllAsync();
            return Ok(appointments);
        }

        [HttpGet("patient/{patientId}")]
        public async Task<IActionResult> GetAppointmentsByPatientId(int patientId)
        {
            var appointments = await _service.GetByPatientIdAsync(patientId);
            if (appointments == null || !appointments.Any())
                return NotFound($"No appointments found for patient with ID {patientId}.");
            return Ok(appointments);
        }

        [HttpGet("caregiver/{caregiverId}")]
        public async Task<IActionResult> GetAppointmentsByCaregiverId(int caregiverId)
        {
            var appointments = await _service.GetByCaregiverIdAsync(caregiverId);
            if (appointments == null || !appointments.Any())
                return NotFound($"No appointments found for caregiver with ID {caregiverId}.");
            return Ok(appointments);
        }

            await _appointmentRepository.CreateAsync(appointment);
            return Ok("Appointment created successfully.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAppointment(int id)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(id);
            if (appointment == null)
                return NotFound($"Appointment with ID {id} not found.");

            return Ok(appointment);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAppointments()
        {
            var appointments = await _appointmentRepository.GetAllAsync();
            return Ok(appointments);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAppointment(int id, [FromBody] Appointment appointment)
        {
            if (appointment == null || id != appointment.Id)
                return BadRequest("Invalid appointment data or ID mismatch.");

            try
            {
                await _service.UpdateAsync(id, appointment);
                return Ok("Appointment updated successfully.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return Ok("Appointment deleted successfully.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            } 


            var existingAppointment = await _appointmentRepository.GetByIdAsync(id);
            if (existingAppointment == null)
                return NotFound($"Appointment with ID {id} not found.");

            await _appointmentRepository.UpdateAsync(id, appointment);
            return Ok("Appointment updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(id);
            if (appointment == null)
                return NotFound($"Appointment with ID {id} not found.");

            await _appointmentRepository.DeleteAsync(id);
            return Ok("Appointment deleted successfully.");

        }
    }
}