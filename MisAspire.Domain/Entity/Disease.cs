﻿namespace MisAspire.Domain.Entity
{
    public class Disease
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public ICollection<PatientDisease> PatientDiseases { get; set; } = [];
    }
}
