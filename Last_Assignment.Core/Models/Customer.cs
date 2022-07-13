namespace Last_Assignment.Core.Models
{
    public class Customer: BaseEntity
    {    
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? PictureUrl { get; set; }       
        public string? City { get; set; }

        //Navigation Property
        public ICollection<CustomerActivity> CustomerActivities { get; set; } 

        public string UserId { get; set; }  
    }
}
