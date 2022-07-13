using System;
using System.Collections.Generic;

namespace FileCreateWorkerService.Models
{
    public partial class CustomerActivity
    {
        public int Id { get; set; }
        public string Service { get; set; } = null!;
        public decimal Amount { get; set; }
        public DateTime ServiceDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; } = null!;
    }
}
