using BananaProtocol.Domain.Enums;

namespace BananaProtocol.Application.Users;

public class UserDto
{
    public int Id { get; set; }
    public string Email { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Address { get; set; }
    public RoleType Role { get; set; }
}