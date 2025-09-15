namespace MisAspire.Domain.Entity
{
    public class Doctor
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Specialty { get; set; } = string.Empty;
        public string LicenseNumber { get; set; } = string.Empty;
        public string ContactNumber { get; set; } = string.Empty;
        public ICollection<Appointment> Appointments { get; set; } = [];
    }
}
