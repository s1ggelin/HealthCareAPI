using System.Collections.Generic;
using System.Threading.Tasks;
using HealthCareABApi.Models;

namespace HealthCareABApi.Repositories
{
    public interface IAvailabilityRepository
    {
        Task<IEnumerable<Availability>> GetAllAsync();                   // Get all availability entries
        Task<Availability> GetByIdAsync(int id);                         // Get an availability entry by its ID
        Task<IEnumerable<Availability>> GetAvailabilitiesAsync(int caregiverId); // Get all availability entries for a specific caregiver
        Task AddAvailabilityAsync(Availability availability);            // Add a new availability entry
        Task DeleteAvailabilityAsync(int id);                            // Delete an availability entry by its ID
    }
}
