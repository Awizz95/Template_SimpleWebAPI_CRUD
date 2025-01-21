namespace Template_SimpleWebAPI_CRUD.Infrastructure
{
    public class JwtConfig
    {
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
        public string? SigningKey { get; set; }
    }
}
