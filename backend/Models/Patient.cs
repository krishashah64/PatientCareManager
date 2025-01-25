using System;

namespace Backend
{
    public class Patient
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Recommendations { get; set; }
        public bool IsComplete { get; set; }  
    }
}
