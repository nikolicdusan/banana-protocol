using BananaProtocol.Application.Common.Mappers;
using BananaProtocol.Contracts.Token;
using Microsoft.AspNetCore.Mvc;

namespace BananaProtocol.API.Token;

public class TokenController : ApiControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateTokenAsync(CreateTokenRequest request, CancellationToken cancellationToken)
    {
        var command = request.ToCommand();
        var result = await Sender.Send(command, cancellationToken);

        return Ok(result);
    }
}