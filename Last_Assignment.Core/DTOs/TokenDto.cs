namespace Last_Assignment.Core.DTOs
{
    public class TokenDto
    {
        public string AccessToken { get; set; }
        public DateTime AccessTokenExpiration { get; set; }
        public string RefreshToken { get; set; } // VT ye kaydedilecek UserRefreshToken Modeli -- Code
        public DateTime RefreshTokenExpiration { get; set; } // VT ye kaydedilecek UserRefreshToken Modeli -- Expiration

    } 
}
