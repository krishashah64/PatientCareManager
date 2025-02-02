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
        public bool IsComplete { get; set; }  

        // New columns
        public string MedicalHistory { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string PhoneNumber { get; set; }
        public string EmergencyContactName { get; set; }
        public string EmergencyContactPhone { get; set; }
        public string Allergies { get; set; }
        public string InsuranceProvider { get; set; }
        public string InsurancePolicyNumber { get; set; }
        public DateTime? LastVisitDate { get; set; }  // Nullable in case no visit has occurred yet
        public string Status { get; set; }  // E.g., Active, Inactive, Discharged
        public DateTime CreatedAt { get; set; } 
        public DateTime UpdatedAt { get; set; }
        public string EmergencyContactRelation { get; set; }
        public string PreferredLanguage { get; set; }

        // Derived properties
        public int Age => DateTime.Now.Year - DateOfBirth.Year;
        public List<PatientRecommendation> Recommendations { get; set; } = new();

    }
}
