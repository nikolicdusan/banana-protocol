using BananaProtocol.Contracts.Enums;

namespace BananaProtocol.Contracts.Resources;

public class CreateUserRequest
{
    public required string Email { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Address { get; set; }
    public RoleType Role => RoleType.Contributor;
}