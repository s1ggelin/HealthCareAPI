using HealthCareABApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthCareABApi.Repositories.Implementations
{
	public class AvailabilityRepository : IAvailabilityRepository
    {
		private readonly AppDbContext _context;

		public AvailabilityRepository(AppDbContext context)
		{
			_context = context;
		}

        public async Task<IEnumerable<Availability>> GetAllAsync()
        {
            return await _context.Availabilities.ToListAsync();
        }

        public async Task<Availability> GetByIdAsync(int id)
        {
            return await _context.Availabilities.FindAsync(id);
        }
        public async Task CreateAsync(Availability availability)
        {
            await _context.Availabilities.AddAsync(availability);
        }

        public async Task UpdateAsync(int id, Availability availability)
        {
            _context.Availabilities.Update(availability);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
			var availability = await _context.Availabilities.FindAsync(id);
			if (availability != null)
			{
				_context.Availabilities.Remove(availability);
				await _context.SaveChangesAsync();
			}
		}
    }
}

