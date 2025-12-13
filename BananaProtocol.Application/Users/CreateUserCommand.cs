using BananaProtocol.Application.Common.Interfaces;
using BananaProtocol.Domain.Entities;
using BananaProtocol.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BananaProtocol.Application.Users;

public record CreateUserCommand(
    string Email,
    string Username,
    string Password,
    string? FirstName,
    string? LastName,
    string? Address,
    RoleType Role) : IRequest<int>;

internal class CreateUserProfileCommandHandler(
    IApplicationDbContext context,
    IPasswordHasher<User> passwordHasher) : IRequestHandler<CreateUserCommand, int>
{
    public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var userExists = await UserExistsAsync(request.Email, cancellationToken);
        if (userExists)
        {
            throw new InvalidOperationException("User with the specified email already exists.");
        }

        var user = await AddUserAsync(request, cancellationToken);

        return user.Id;
    }

    #region Private Functions

    private async Task<bool> UserExistsAsync(string email, CancellationToken cancellationToken) =>
        await context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == email, cancellationToken) !=
        null;

    private async Task<User> AddUserAsync(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User
        {
            Email = request.Email,
            Username = request.Username,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Address = request.Address,
            Role = request.Role
        };

        var hashedPassword = passwordHasher.HashPassword(user, request.Password);
        user.PasswordHash = hashedPassword;

        context.Users.Add(user);
        await context.SaveChangesAsync(cancellationToken);
        
        return user;
    }

    #endregion
}