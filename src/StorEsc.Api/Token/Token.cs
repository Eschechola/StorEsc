namespace StorEsc.API.Token;

public class Token
{
    public string Alg { get; set; }
    public string Type { get; private set; }
    public string Issuer { get; private set; }
    public string Value { get; private set; }
    public DateTime ExpiresAt { get; private set; }
    public DateTime IssuedAt { get; private set; }

    public Token(
        string value,
        string issuer,
        DateTime issuedAt,
        int hoursToExpire)
    {
        Alg = "HS256";
        Type = "JWT";
        Value = value;
        Issuer = issuer;
        IssuedAt = issuedAt;
        ExpiresAt = issuedAt.AddHours(hoursToExpire);
    }
}