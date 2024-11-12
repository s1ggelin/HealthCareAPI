using HealthCareABApi.Models;
using HealthCareABApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Reflection.Emit;
using System.Text.Json;

namespace HealthCareABApi.Repositories.Implementations
{
	public class AppDbContext : DbContext, IAppDbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.UseSerialColumns();

			var rolesConverter = new ValueConverter<List<string>, string>(
			v => JsonSerializer.Serialize(v, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
			v => JsonSerializer.Deserialize<List<string>>(v, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<string>());

			modelBuilder.Entity<User>()
			.Property(e => e.Roles)
			.HasConversion(rolesConverter)
			.HasColumnType("jsonb");
		}

		public DbSet<User> Users { get; set; }
		public DbSet<Appointment> Appointments { get; set; }
		public DbSet<Availability> Availabilities { get; set; }
		public DbSet<Feedback> Feedbacks { get; set; }

		public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			return base.SaveChangesAsync(cancellationToken);
		}
	}
}
