namespace Ratiu_Ruxandra_Proiect.Models
{
    public class NoShowApiInput
    {
        public float patientAge { get; set; }
        public string dayOfWeek { get; set; } = string.Empty;
        public float hour { get; set; }
        public string doctorSpecialization { get; set; } = string.Empty;
        public string serviceName { get; set; } = string.Empty;

        // NOI
        public float hasReminder { get; set; }        // 0 sau 1
        public float previousNoShows { get; set; }    // 0..3
    }
}
