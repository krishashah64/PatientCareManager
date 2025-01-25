using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        // In-memory list of patients 
        private static readonly List<Patient> Patients = new List<Patient>
        {
            new Patient { Id = 1, FirstName = "John", LastName = "Doe", DateOfBirth = new DateTime(1990, 1, 1), Gender = "Male", Recommendations = "Allergy Check", IsComplete = false },
            new Patient { Id = 2, FirstName = "Jane", LastName = "Smith", DateOfBirth = new DateTime(1985, 5, 23), Gender = "Female", Recommendations = "Screening", IsComplete = false },
            new Patient { Id = 3, FirstName = "Emily", LastName = "Johnson", DateOfBirth = new DateTime(1989, 10, 23), Gender = "Female", Recommendations = "Allergy Check", IsComplete = false },
            new Patient { Id = 4, FirstName = "Michael", LastName =  "Brown", DateOfBirth = new DateTime(1992, 8, 11), Gender = "Male", Recommendations = "Follow-up", IsComplete = false }
        };

        // GET: api/patients
       [HttpGet(Name = "GetPatients")]
        public IEnumerable<Patient> Get()
        {
            return Patients; // Return the in-memory list of patients
        }

    }
}

     
