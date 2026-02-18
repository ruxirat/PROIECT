using System.Collections.Generic;

namespace Ratiu_Ruxandra_Proiect.Models
{
    public class Doctor
    {
        public int DoctorId { get; set; }   // PK
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public int? SpecializationId { get; set; }   // FK
        public Specialization? Specialization { get; set; }

        public ICollection<Appointment>? Appointments { get; set; }
    }
}
