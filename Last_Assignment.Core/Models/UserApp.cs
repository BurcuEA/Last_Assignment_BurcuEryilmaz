using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Last_Assignment.Core.Models
{
    public class UserApp : IdentityUser
    {
        public string City { get; set; }
    }





    //// Sil ya da Düzeltme yapabiliyorsan YAP ?? !!!

    ////Identity Role ekleme ... ÖNEMLİİ... // 38. video Start Up 1  13:17 dk sn ... (E) UserApp olduğu class a git ..
    //public class UserRole : IdentityRole
    //{
    //    //public int MyProperty { get; set; }  IdentityRole tablosuna Hangi sütunu  ne eklemek istiyorsan
    //}



}
