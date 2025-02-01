namespace Backend
{
    public class PatientFile
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; } // Navigation property to the Patient

        public string FileName { get; set; } // Name of the file
        public string FilePath { get; set; } // Path where the file is stored
        public string FileType { get; set; } // E.g., "Lab Report", "X-Ray Image"
        public DateTime UploadedAt { get; set; } // Date when the file was uploaded
    }
}
