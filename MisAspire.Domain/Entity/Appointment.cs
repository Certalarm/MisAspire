using System.Text.Json.Serialization;

namespace MisAspire.Domain.Entity
{
    public class Appointment
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        [JsonIgnore]
        public Patient? Patient { get; set; }
        public int DoctorId { get; set; }
        [JsonIgnore]
        public Doctor? Doctor { get; set; }
        public DateTime ScheduleTime { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
    }
}
