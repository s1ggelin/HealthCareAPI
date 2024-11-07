using System;
using HealthCareABApi.Models;
using MongoDB.Driver;

namespace HealthCareABApi.Repositories.Implementations
{
    public class MongoDbContext : IMongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MongoDb");
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(configuration["MongoDbSettings:DatabaseName"]);
        }

        public IMongoCollection<Appointment> Appointments => _database.GetCollection<Appointment>("Appointments");
        public IMongoCollection<Availability> Availabilities => _database.GetCollection<Availability>("Availabilities");
        public IMongoCollection<Feedback> Feedbacks => _database.GetCollection<Feedback>("Feedbacks");
    }
}

