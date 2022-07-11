namespace SharedLibrary.Dtos
{
    public class ErrorDto
    {
        public List<String> Errors { get; private set; } = new List<string>(); // (AA) Burda new liyoruz ki tüm ((AA) yazılan yerde new lenebilsin...)
        public bool IsShow { get; private set; }

        //public ErrorDto()
        //{ 
        //    Errors = new List<string>(); (AA)
        //}

        public ErrorDto(string error, bool isShow)
        {
            //  (AA)
            Errors.Add(error);
            IsShow = isShow;
        }
        public ErrorDto(List<string> errors, bool isShow)
        {
            Errors = errors;
            IsShow = isShow;
        }
    }
}
