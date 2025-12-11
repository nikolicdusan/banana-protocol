namespace BananaProtocol.Application.Common.Interfaces;

public interface ITokenProvider
{
    public string GenerateToken(Guid userId, string email, IEnumerable<string>? roles = null);
}