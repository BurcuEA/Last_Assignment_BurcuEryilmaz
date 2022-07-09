using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Last_Assignment.Core.DTOs
{
    // DTo --- client lara gidecek model
    public class CreateUserDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string City { get; set; } // 43. video Postman Endpoint  işlemi kısmında hata verdiği için ekledik
    }
}
