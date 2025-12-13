using BananaProtocol.Domain.Enums;

namespace BananaProtocol.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public required string Email { get; set; }
    public required string Username { get; set; }
    public string? PasswordHash { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; init; }
    public string? Address { get; set; }
    public RoleType Role { get; set; }
}