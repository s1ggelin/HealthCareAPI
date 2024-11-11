﻿using System;
using HealthCareABApi.Models;
using HealthCareABApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace HealthCareABApi.Repositories.Implementations
{
	public class AppointmentRepository : IAppointmentRepository
	{
		private readonly AppDbContext _context;

		public AppointmentRepository(AppDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Appointment>> GetAllAsync()
		{
			return await _context.Appointments
			   .Include(a => a.PatientId)
			   .Include(a => a.CaregiverId)
			   .ToListAsync();
		}

		public async Task<Appointment> GetByIdAsync(int id)
		{
			return await _context.Appointments
	 .Include(a => a.PatientId)
	 .Include(a => a.CaregiverId)
	 .FirstOrDefaultAsync(a => a.Id == id);
		}

		public async Task CreateAsync(Appointment appointment)
		{
			await _context.Appointments.AddAsync(appointment);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateAsync(int id, Appointment appointment)
		{
			_context.Appointments.Update(appointment);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(int id)
		{
			var appointment = await _context.Appointments.FindAsync(id);
			if (appointment != null)
			{
				_context.Appointments.Remove(appointment);
				await _context.SaveChangesAsync();
			}
		}
	}
}
