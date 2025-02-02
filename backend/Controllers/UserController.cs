using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Backend.Data; 
using Microsoft.EntityFrameworkCore; 

namespace Backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }


        // GET: api/users
        [HttpGet(Name = "GetUser")]
        public async Task<IEnumerable<User>> Get()
        {
            // Retrieve all users from the database
            return await _context.Users.ToListAsync();
        }

    }
}

     
