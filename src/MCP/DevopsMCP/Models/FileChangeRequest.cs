namespace DevopsMCP.Models
{
    public class FileChangeRequest
    {
        public string Path { get; set; } = string.Empty;
        public string ChangeType { get; set; } = string.Empty; // add, edit, delete
        public string? Content { get; set; }
        public string? Encoding { get; set; } = "base64";
    }
}
