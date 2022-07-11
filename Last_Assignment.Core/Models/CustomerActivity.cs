using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Last_Assignment.Core.Models
{
    public class CustomerActivity : BaseEntity
    {      
        public string Service { get; set; }
        public decimal Amount  { get; set; }
        public DateTime ServiceDate { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } 


    }
}
