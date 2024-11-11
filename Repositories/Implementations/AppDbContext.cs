using HealthCareABApi.Models;
using HealthCareABApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HealthCareABApi.Repositories.Implementations
{
	public class AppDbContext : DbContext, IAppDbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}

		public DbSet<Appointment> Appointments { get; set; }
		public DbSet<Availability> Availabilities { get; set; }
		public DbSet<Feedback> Feedbacks { get; set; }
	}
}
