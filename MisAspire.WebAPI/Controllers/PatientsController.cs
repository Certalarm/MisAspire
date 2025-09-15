using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MisAspire.Domain.Entity;
using MisAspire.Persistence.Context;

namespace MisAspire.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase
    {
        private const string __patientsInfo = "Getting all patients";
        private const string __patientsError = "Error getting patients";
        private const string __patientInfo = "Getting patient with ID: {PatientId}";
        private const string __patientError = "Error getting patient with ID: {PatientId}";

        private readonly MisContext _context;
        private readonly ILogger<PatientsController> _logger;

        public PatientsController(MisContext context, ILogger<PatientsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patient>>> GetAllPatients()
        {
            _logger.LogInformation(__patientsInfo);
            try
            {
                var patients = await _context.Patients
                    .AsNoTracking()
                    .Include(x => x.Appointments)
                    .Include(x => x.PatientDiseases)
                    .ToListAsync();
                return Ok(patients);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, __patientsError);
                throw;
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> GetPatient(int id)
        {
            _logger.LogInformation(__patientInfo, id);
            try
            {
                var patient = await _context.Patients
                    .AsNoTracking()
                    .Include(x => x.Appointments)
                    .Include(x => x.PatientDiseases)
                    .FirstOrDefaultAsync(p => p.Id == id);
                return patient == null
                    ? NotFound()
                    : Ok(patient);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, __patientError, id);
                throw;
            }
        }
    }
}
