using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Last_Assignment.Core.Models
{
    public class UserRefreshToken
    {
        public string UserId { get; set; }
        public string Code { get; set; } // RefreshToken  -- TokenDto daki
        public DateTime Expiration { get; set; } // RefreshTokenExpiration -- TokenDto daki
    }
}
