using BananaProtocol.Application.Common.Exceptions;
using BananaProtocol.Application.Common.Interfaces;
using BananaProtocol.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BananaProtocol.Application.Token;

public record CreateTokenCommand(string Email, string Password) : IRequest<string>;

internal class CreateTokenCommandHandler(IApplicationDbContext context, ITokenProvider tokenProvider) : IRequestHandler<CreateTokenCommand, string>
{
    public async Task<string> Handle(CreateTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await context.Users
            .FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

        if (user is null)
        {
            throw new NotFoundException(nameof(User), request.Email);
        }

        return tokenProvider.GenerateToken(user.Id, user.Email);
    }
}