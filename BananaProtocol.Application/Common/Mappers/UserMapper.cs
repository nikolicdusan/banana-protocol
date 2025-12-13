using BananaProtocol.Application.Users;
using BananaProtocol.Contracts.Resources;
using BananaProtocol.Domain.Entities;
using BananaProtocol.Domain.Enums;

namespace BananaProtocol.Application.Common.Mappers;

public static class UserMapper
{
    public static CreateUserCommand ToCommand(this CreateUserRequest request) =>
        new(
            request.Email,
            request.Username,
            request.Password,
            request.FirstName,
            request.LastName,
            request.Address,
            (RoleType)request.Role);

    public static UserDto ToDto(this User user) =>
        new()
        {
            Id = user.Id,
            Email = user.Email,
            Username = user.Username,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Address = user.Address,
            Role = user.Role.ToString(),
        };
}