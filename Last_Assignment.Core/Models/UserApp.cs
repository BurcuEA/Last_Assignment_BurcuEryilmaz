using Microsoft.AspNetCore.Identity;

namespace Last_Assignment.Core.Models
{
    public class UserApp : IdentityUser
    {
        public string City { get; set; }
    }
}
