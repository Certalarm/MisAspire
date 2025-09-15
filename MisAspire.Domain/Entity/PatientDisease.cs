using System.Text.Json.Serialization;

namespace MisAspire.Domain.Entity
{
    public class PatientDisease
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        [JsonIgnore]
        public Patient? Patient { get; set; }
        public int DiseaseId { get; set; }
        [JsonIgnore]
        public Disease? Disease { get; set; }
        public DateTime DiagnosedDate { get; set; }
        public DateTime? RecoveryDate { get; set; }
        public string TreatmentNotes { get; set; } = string.Empty;
    }
}
