namespace SharedLibrary.Configurations
{
    public class CustomTokenOption
    {
        //appsettings'de TokenOption'a karşılık gelen class  
        public List<String> Audience { get; set; }
        public string Issuer { get; set; }
        public int AccessTokenExpiration { get; set; }
        public int RefreshTokenExpiration { get; set; }
        public string SecurityKey { get; set; }
    }
}
