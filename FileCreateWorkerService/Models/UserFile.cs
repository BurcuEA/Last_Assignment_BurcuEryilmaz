using System;
using System.Collections.Generic;

namespace FileCreateWorkerService.Models
{
    public partial class UserFile
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public string FileName { get; set; } = null!;
        public string FilePath { get; set; } = null!;
        public int FileStatus { get; set; }
    }
}
