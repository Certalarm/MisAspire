﻿namespace MisAspire.Domain.Entity
{
    public class Patient
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; } = string.Empty ;
        public string ContactNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string InsuranceNumber { get; set; } = string.Empty;
        public ICollection<PatientDisease> PatientDiseases { get; set; } = [];
        public ICollection<Appointment> Appointments { get; set; } = [];
    }
}
