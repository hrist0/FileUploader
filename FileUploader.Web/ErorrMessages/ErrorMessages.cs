namespace FileUploader.Web.ErorrMessages
{
    public static class ErrorMessages
    {
        public const string InvalidCredentials = "Invalid email or password.";
        public const string InvalidToken = "Invalid token.";
        public const string NoFileUploaded = "No file uploaded.";
        public const string FileSizeExceedsLimit = "File size exceeds the limit.";
        public const string InvalidFileType = "Only images (JPEG, PNG) and PDFs are allowed.";
        public const string FailedToUploadFile = "Failed to upload file.";
        public const string InternalServerError = "Internal server error: {0}";
    }
}
