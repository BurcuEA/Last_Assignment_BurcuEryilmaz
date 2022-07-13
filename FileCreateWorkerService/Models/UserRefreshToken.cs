using System;
using System.Collections.Generic;

namespace FileCreateWorkerService.Models
{
    public partial class UserRefreshToken
    {
        public string UserId { get; set; } = null!;
        public string Code { get; set; } = null!;
        public DateTime Expiration { get; set; }
    }
}
