namespace Backend
{
    public class PatientMedicalHistory
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; } // Navigation property to the Patient

        public string MedicalCondition { get; set; } 
        public string Treatment { get; set; } // Description of the treatment
        public DateTime DiagnosisDate { get; set; } // When the condition was diagnosed
        public DateTime TreatmentStartDate { get; set; } // When treatment began
        public DateTime? TreatmentEndDate { get; set; } // When treatment ended, if applicable
    }
}
