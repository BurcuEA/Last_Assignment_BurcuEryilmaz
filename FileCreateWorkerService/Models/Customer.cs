using System;
using System.Collections.Generic;

namespace FileCreateWorkerService.Models
{
    public partial class Customer
    {
        public Customer()
        {
            CustomerActivities = new HashSet<CustomerActivity>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? PictureUrl { get; set; }
        public string? City { get; set; }
        public string UserId { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual ICollection<CustomerActivity> CustomerActivities { get; set; }
    }
}
