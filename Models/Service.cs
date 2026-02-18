namespace Ratiu_Ruxandra_Proiect.Models
{
    public class Service
    {
        public int ServiceId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int DurationMinutes { get; set; }
        public decimal Price { get; set; }

        // ✅ adaugi asta:
        public int? SpecializationId { get; set; }
        public Specialization? Specialization { get; set; }

        public ICollection<Appointment>? Appointments { get; set; }
    }
}
