namespace Backend
{
    public class PatientRecommendation
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; } // Navigation property to the Patient

        public string RecommendationType { get; set; } 
        public string Details { get; set; } // Description of the recommendation
        public bool IsCompleted { get; set; } // Track completion status
        public DateTime RecommendationDate { get; set; } // Date the recommendation was made

    }
}
