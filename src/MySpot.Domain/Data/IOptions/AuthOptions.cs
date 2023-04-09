namespace MySpot.Domain.Data.IOptions;

public class AuthOptions
{
    public const string SectionName = "Auth";
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string SigningKey { get; set; }
    public TimeSpan? Expiry { get; set; }
}