namespace BananaProtocol.Contracts.Resources;

public class CreateTokenRequest
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}