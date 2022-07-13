namespace Last_Assignment.Core.DTOs
{
    public class LoginDto
    {
        // Request te bu bilgiler gelecek ve VT deki ile eşleşirse de token dönülecek
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
