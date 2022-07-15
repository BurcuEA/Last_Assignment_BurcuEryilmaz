namespace SharedLibrary.Dtos.ExcelReportsDtos
{
    public class GeneralReportListDto
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public int Count { get; set; }
        public decimal TotalAmount { get; set; }
    }  
}
