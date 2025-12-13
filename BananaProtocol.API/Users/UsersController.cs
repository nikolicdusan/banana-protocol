using BananaProtocol.Application.Common.Mappers;
using BananaProtocol.Application.Users;
using BananaProtocol.Contracts.Resources;
using Microsoft.AspNetCore.Mvc;

namespace BananaProtocol.API.Users;

public class UsersController : ApiControllerBase
{
    [HttpGet("{id:int}", Name = nameof(GetUserById))]
    public async Task<IActionResult> GetUserById(
        int id,
        CancellationToken cancellationToken)
    {
        var query = new GetUserByIdQuery(id);
        var result = await Sender.Send(query, cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(
        CreateUserRequest request,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand();
        var result = await Sender.Send(command, cancellationToken);

        return CreatedAtAction(
            nameof(GetUserById),
            routeValues: new { id = result },
            result);
    }
}