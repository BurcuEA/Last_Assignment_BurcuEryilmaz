using Last_Assignment.Core.Models;

namespace Last_Assignment.Core.DTOs
{
    public class UserFileDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public FileStatus FileStatus { get; set; }
    }
}
