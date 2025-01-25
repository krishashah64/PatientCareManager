using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // In-memory list of patients (this would usually come from a database)
        private static readonly List<User> User = new List<User>
        {
            new User { Id = 1, FirstName = "Terry", LastName = "Smith", DateOfBirth = new DateTime(1990, 1, 1), Gender = "Male", Role = "Admin" },
            new User { Id = 2, FirstName = "Angel", LastName = "Johnson", DateOfBirth = new DateTime(1985, 5, 23), Gender = "Female", Role = "Nurse"},
            new User { Id = 3, FirstName = "Emily", LastName = "Smith", DateOfBirth = new DateTime(1989, 10, 23), Gender = "Female", Role = "Nurse" },
            new User { Id = 4, FirstName = "Mellisa", LastName =  "Brown", DateOfBirth = new DateTime(1992, 8, 11), Gender = "Female", Role = "Nurse" }
        };

        // GET: api/users
       [HttpGet(Name = "GetUser")]
        public IEnumerable<User> Get()
        {
            return User; 
        }

    }
}

     
