using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Last_Assignment.Core.DTOs
{
    // Hava Durumu-- üyelik sistemi istemeyen API için idi. KALDIRILACAK MIII kontrol et


    public class ClientTokenDto
    {
        public string AccessToken { get; set; }
        public DateTime AccessTokenExpiration { get; set; }
    }

}
