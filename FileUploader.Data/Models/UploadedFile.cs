namespace FileUploader.Data.Models
{
    public class UploadedFile
    {
        public int Id { get; set; }

        public string FileName { get; set; }

        public string FilePath { get; set; }

        public int UserId { get; set; } // Foreign key to link file to user

        public User User { get; set; }   // Navigation property
    }
}
