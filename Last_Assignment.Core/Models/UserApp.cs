using Microsoft.AspNetCore.Identity;

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
    //    public string Description { get; set; } // IdentityRole tablosuna Hangi sütunu  ne eklemek istiyorsan
    //public string Description { get; set; }
    //}



}
