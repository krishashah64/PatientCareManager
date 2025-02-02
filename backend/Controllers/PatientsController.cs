using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PatientsController(AppDbContext context)
        {
            _context = context;
        }

        // Get All Patients with Recommendations
        [HttpGet]
        public async Task<IActionResult> GetPatients(
            [FromQuery] int page = 1, 
            [FromQuery] int pageSize = 10, 
            [FromQuery] string search = "")
            {
             var query = _context.Patients
                .Include(p => p.Recommendations) // Ensure navigation property is included
                .AsQueryable();

            int totalPatients = await query.CountAsync();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.FirstName.Contains(search) ||
                                         p.LastName.Contains(search) ||
                                         p.Id.ToString().Contains(search));
            }

            var patients = await query
                .OrderBy(p => p.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            Console.WriteLine("response Patients--", patients);

            var result = patients.Select(p => new
            {
                p.Id,
                p.FirstName,
                p.LastName,
                p.DateOfBirth,
                p.Gender,
                Recommendations = p.Recommendations.Select(r => new
                {
                    r.Id,
                    r.RecommendationType,
                    r.Details,
                    r.IsCompleted
                }).ToList()
            });

            return Ok(new
            {
                patients = result,
                totalPages = (int)Math.Ceiling((double)totalPatients / pageSize),
                currentPage = page
            });
        }

        // ✅ Get a Single Patient with Recommendations
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatientDetails(int id)
        {
            var patient = await _context.Patients
                .Include(p => p.Recommendations) // ✅ Fetch Recommendations
                .FirstOrDefaultAsync(p => p.Id == id);

            if (patient == null)
            {
                return NotFound(new { Message = "Patient not found." });
            }

            return Ok(new
            {
                patient.Id,
                patient.FirstName,
                patient.LastName,
                patient.DateOfBirth,
                patient.Gender,
                patient.Address,
                patient.PhoneNumber,
                patient.MedicalHistory,
                Recommendations = patient.Recommendations.Select(r => new
                {
                    r.Id,
                    r.RecommendationType,
                    r.Details,
                    r.IsCompleted
                }).ToList()
            });
        }

      //  ✅ Update Recommendation Status
        [HttpPut("recommendation/{id}")]
        public async Task<IActionResult> UpdateRecommendationStatus(int id, [FromBody] bool isCompleted)
        {
            var recommendation = await _context.PatientRecommendations.FindAsync(id);

            if (recommendation == null)
            {
                return NotFound(new { Message = "Recommendation not found." });
            }

            recommendation.IsCompleted = isCompleted;
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Recommendation status updated." });
        }
    }
}
