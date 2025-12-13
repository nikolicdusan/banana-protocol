namespace BananaProtocol.Infrastructure.Options;

public sealed class JwtOptions
{
    public string Issuer { get; init; }
    public string Audience { get; init; }
    public string SigningKey { get; init; }
    public int ExpirationMinutes { get; init; }
}