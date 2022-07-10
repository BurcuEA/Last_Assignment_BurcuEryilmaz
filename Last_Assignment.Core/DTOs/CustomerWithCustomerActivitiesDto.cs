namespace Last_Assignment.Core.DTOs
{
    public class CustomerWithCustomerActivitiesDto : CustomerDto
    {
        public List<CustomerActivityDto> CustomerActivities { get; set; }
    }
}
