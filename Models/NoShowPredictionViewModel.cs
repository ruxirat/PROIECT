namespace Ratiu_Ruxandra_Proiect.Models
{
    public class NoShowPredictionViewModel
    {
        public float PatientAge { get; set; }
        public string DayOfWeek { get; set; } = string.Empty;
        public float Hour { get; set; }
        public string DoctorSpecialization { get; set; } = string.Empty;
        public string ServiceName { get; set; } = string.Empty;

        // NOI
        public float HasReminder { get; set; }
        public float PreviousNoShows { get; set; }

        public int? PredictedLabel { get; set; }
        public float? ProbabilityNo { get; set; }
        public float? ProbabilityYes { get; set; }
    }
}
