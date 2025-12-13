namespace BananaProtocol.Application.Common.Interfaces;

public interface ITokenProvider
{
    public string GenerateToken(object userId, string email, IEnumerable<string>? roles = null);
}