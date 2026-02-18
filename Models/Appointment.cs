using System;

namespace Ratiu_Ruxandra_Proiect.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }  // PK

        public int? PatientId { get; set; }
        public int? DoctorId { get; set; }
        public int? ServiceId { get; set; }

        public Patient? Patient { get; set; }
        public Doctor? Doctor { get; set; }
        public Service? Service { get; set; }

        public DateTime DateTime { get; set; }
        public string Status { get; set; } = "Scheduled";
        public string? Notes { get; set; }
    }
}
