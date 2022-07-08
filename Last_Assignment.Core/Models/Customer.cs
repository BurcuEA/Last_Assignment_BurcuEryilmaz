using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Last_Assignment.Core.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PictureUrl { get; set; } // xxxx       
        public string City { get; set; }

        //Navigation Property
        //Customer ın 1 den fazla  CustomerActivity si olabilir
        public ICollection<CustomerActivity> CustomerActivities { get; set; } // xxxx  


        public string UserId { get; set; } // idetity  ID sting gelcek Bu ürün kime ait ten yola çıkarak bu customer kim 
    }
}
