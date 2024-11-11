namespace HealthCareABApi.Models
{
	public class Availability
    {
        public int Id { get; set; }
        public int CaregiverId { get; set; }
        public List<DateTime> AvailableSlots { get; set; }
    }
}

