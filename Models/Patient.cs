using System;
using System.Collections.Generic;

namespace Ratiu_Ruxandra_Proiect.Models
{
    public class Patient
    {
        public int PatientId { get; set; }   // PK
        public string FullName { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public DateTime? BirthDate { get; set; }

        public ICollection<Appointment>? Appointments { get; set; }
    }
}
