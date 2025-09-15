using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MisAspire.Domain.Entity;
using MisAspire.Persistence.Context;

namespace MisAspire.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorsController : ControllerBase
    {
        private const string __doctorsInfo = "Getting all doctors";
        private const string __doctorsError = "Error getting doctors";
        private const string __specialtyInfo = "Getting doctors with specialty: {Specialty}";
        private const string __specialtyError = "Error getting doctors by specialty: {Specialty}";

        private readonly MisContext _context;
        private readonly ILogger<DoctorsController> _logger;

        public DoctorsController(MisContext context, ILogger<DoctorsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetAllDoctors()
        {
            _logger.LogInformation(__doctorsInfo);
            try
            {
                var doctors = await _context.Doctors
                    .AsNoTracking()
                    .Include(x => x.Appointments)
                    .ToListAsync();
                return Ok(doctors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, __doctorsError);
                throw;
            }
        }

        [HttpGet("specialty/{specialty}")]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetDoctorsBySpecialty(string specialty)
        {
            _logger.LogInformation(__specialtyInfo, specialty);
            try
            {
                var doctors = await _context.Doctors
                    .AsNoTracking()
                    .Where(d => string.Equals(d.Specialty.ToLower(), specialty.ToLower()))
                    .ToListAsync();
                return Ok(doctors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, __specialtyError, specialty);
                throw;
            }
        }
    }
}
