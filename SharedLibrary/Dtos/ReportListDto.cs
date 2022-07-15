namespace SharedLibrary
{
    public class ReportListDto
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public int Count { get; set; }
        public decimal TotalAmount { get; set; }
    }

    public class MontlyReportListDto
    {
        public string City { get; set; }
        public int CustomerCount { get; set; }
    }

    public class WeeklyReportListDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int TotalActivityCount { get; set; }
    }

    public class GetSamePhoneDifferentNameReportListDto
    {
        public int Count { get; set; }
        public string PhoneNumber { get; set; }
    }
}
