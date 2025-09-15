using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MisAspire.Domain.Entity;
using MisAspire.Persistence.Context;

namespace MisAspire.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiseasesController : ControllerBase
    {
        private const string __diseasesInfo = "Getting all diseases";
        private const string __diseasesError = "Error getting diseases";

        private readonly MisContext _context;
        private readonly ILogger<DiseasesController> _logger;

        public DiseasesController(MisContext context, ILogger<DiseasesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Disease>>> GetAllDiseases()
        {
            _logger.LogInformation(__diseasesInfo);
            try
            {
                var diseases = await _context.Diseases
                    .AsNoTracking()
                    .Include(x=> x.PatientDiseases)
                    .ToListAsync();
                return Ok(diseases);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, __diseasesError);
                throw;
            }
        }
    }
}
