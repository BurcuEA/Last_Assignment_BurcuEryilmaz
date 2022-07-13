namespace Last_Assignment.Core.DTOs
{
    public class CustomerActivityDto
    {
        public int Id { get; set; } 
        public string Service { get; set; }
        public decimal Amount { get; set; }
        public int CustomerId { get; set; }        
    }
}
