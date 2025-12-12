using BananaProtocol.Application.Common.Exceptions;
using BananaProtocol.Application.Common.Interfaces;
using BananaProtocol.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BananaProtocol.Application.Token;

public record CreateTokenCommand(string Email, string Password) : IRequest<string>;

internal class CreateTokenCommandHandler(
    IApplicationDbContext context,
    ITokenProvider tokenProvider,
    IPasswordHasher<User> passwordHasher) : IRequestHandler<CreateTokenCommand, string>
{
    public async Task<string> Handle(CreateTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await GetUserAsync(request.Email);
        VerifyPassword(user, request.Password);

        return tokenProvider.GenerateToken(user.Id, user.Email);
    }

    private async Task<User> GetUserAsync(string email) =>
        await context.Users.FirstOrDefaultAsync(u => u.Email == email) ?? throw new NotFoundException(nameof(User), email);

    private void VerifyPassword(User user, string providedPassword)
    {
        var isPasswordValid = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, providedPassword) !=
            PasswordVerificationResult.Failed;
        if (!isPasswordValid)
        {
            throw new InvalidCredentialsException("Invalid credentials");
        }
    }
}