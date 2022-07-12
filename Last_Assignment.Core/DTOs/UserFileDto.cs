using Last_Assignment.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Last_Assignment.Core.DTOs
{
    public class UserFileDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        //public DateTime? CreatedDate { get; set; }
        public FileStatus FileStatus { get; set; }


        //[NotMapped]
        //public string GetCreatedDate => CreatedDate.HasValue ? CreatedDate.Value.ToShortDateString() : "-";


    }
}
