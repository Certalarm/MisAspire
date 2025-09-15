using MisAspire.Domain.Entity;
using MisAspire.Persistence.Context;

namespace MisAspire.Persistence.Extensions
{
    public static class MisContextExtensions
    {
        public static void SeedStartData(this MisContext context)
        {
            var testPatient = context.Patients
                .FirstOrDefault(b => b.Id == 1);
            if (testPatient != null)
                return;

            context.Patients.AddRange(CreatePatients());
            context.Doctors.AddRange(CreateDoctors());
            context.Diseases.AddRange(CreateDiseases());
            context.Appointments.AddRange(CreateAppointments());
            context.PatientDiseases.AddRange(CreatePatientDiseases());
            context.SaveChanges();
        }

        private static IEnumerable<Patient> CreatePatients()
        {
            yield return new Patient
            {
                Id = 1,
                FirstName = "Петя",
                LastName = "Иванов",
                Address = "Яблочная 1",
                BirthDate = DateTime.UtcNow.AddYears(-30),
                ContactNumber = "+79999999999",
                Email = "pivanov@ya.ru",
                InsuranceNumber = "99-9999-99999",
                Gender = "Мужской"
            };
            yield return new Patient
            {
                Id = 2,
                FirstName = "Люба",
                LastName = "Петрова",
                Address = "Грушевая 2",
                BirthDate = DateTime.UtcNow.AddYears(-18),
                ContactNumber = "+78888888888",
                Email = "yupetrova@mail.ru",
                InsuranceNumber = "88-8888-88888",
                Gender = "Женский"
            };
            yield return new Patient
            {
                Id = 3,
                FirstName = "Витя",
                LastName = "Сидоров",
                Address = "Виноградная 3",
                BirthDate = DateTime.UtcNow.AddYears(-44),
                ContactNumber = "+77777777777",
                Email = "vsidorov@gmail.com",
                InsuranceNumber = "77-7777-77777",
                Gender = "Мужской"
            };
        }

        private static IEnumerable<Doctor> CreateDoctors()
        {
            yield return new Doctor
            {
                Id = 1,
                FirstName = "Пиля",
                LastName = "Пилюлькин",
                ContactNumber = "71111111111",
                LicenseNumber = "111-111-111",
                Specialty = "Хирург"                
            };
            yield return new Doctor
            {
                Id = 2,
                FirstName = "Кузя",
                LastName = "Клизьмин",
                ContactNumber = "72222222222",
                LicenseNumber = "222-222-222",
                Specialty = "Терапевт"

            };
            yield return new Doctor
            {
                Id = 3,
                FirstName = "Гера",
                LastName = "Грелкин",
                ContactNumber = "73333333333",
                LicenseNumber = "333-333-333",
                Specialty = "Педиатр"
            };
        }

        private static IEnumerable<Disease> CreateDiseases()
        {
            yield return new Disease
            {
                Id = 1,
                Name = "Грипп",
                Description = "Острое респираторное заболевание",
                Category = "ОРЗ"
            };
            yield return new Disease
            {
                Id = 2,
                Name = "Аллергия",
                Description = "Реакция иммунной системы",
                Category = "РИС"
            };
            yield return new Disease
            {
                Id = 3,
                Name = "Бронхит",
                Description = "Острое респираторная вирусная инфекция",
                Category = "ОРВИ"
            };
        }

        private static IEnumerable<Appointment> CreateAppointments()
        {
            yield return new Appointment
            {
                Id = 1,
                DoctorId = 1,
                PatientId = 1,
                ScheduleTime = DateTime.UtcNow.AddDays(-5),
                Status = "Выполнено",
                Notes = "Открывался больничный"
            };
            yield return new Appointment
            {
                Id = 2,
                DoctorId = 2,
                PatientId = 2,
                ScheduleTime = DateTime.UtcNow.AddDays(-7),
                Status = "Выполнено",
                Notes = "Открывался больничный"
            };
            yield return new Appointment
            {
                Id = 3,
                DoctorId = 3,
                PatientId = 3,
                ScheduleTime = DateTime.UtcNow.AddDays(-15),
                Status = "Выполнено",
                Notes = "Не открывался больничный"
            };
        }

        private static IEnumerable<PatientDisease> CreatePatientDiseases()
        {
            yield return new PatientDisease
            {
                DiagnosedDate = DateTime.UtcNow.AddDays(-5),
                DiseaseId = 1,
                PatientId = 1,
                RecoveryDate = DateTime.UtcNow.AddDays(-2),
                TreatmentNotes = "Анальгин, 3р в д"
            };
            yield return new PatientDisease
            {
                DiagnosedDate = DateTime.UtcNow.AddDays(-7),
                DiseaseId = 2,
                PatientId = 2,
                RecoveryDate = DateTime.UtcNow.AddDays(-3),
                TreatmentNotes = "Аспирин 2р в д"
            };
            yield return new PatientDisease
            {
                DiagnosedDate = DateTime.UtcNow.AddDays(-9),
                DiseaseId = 3,
                PatientId = 3,
                RecoveryDate = DateTime.UtcNow.AddDays(-4),
                TreatmentNotes = "Цитрамон, 5р в д"
            };
        }
    }
}
