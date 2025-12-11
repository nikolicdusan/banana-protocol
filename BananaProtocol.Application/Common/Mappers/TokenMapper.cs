using BananaProtocol.Application.Token;
using BananaProtocol.Contracts.Token;

namespace BananaProtocol.Application.Common.Mappers;

public static class TokenMapper
{
    public static CreateTokenCommand ToCommand(this CreateTokenRequest request) =>
        new CreateTokenCommand(request.Email, request.Password);
}