using Microsoft.AspNetCore.Mvc;

namespace BananaProtocol.API.Ping;

public class PingController(ILogger<PingController> logger) : ApiControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Ping()
    {
        logger.LogInformation("Running health check...");

        // Simulate health checks
        await Task.Delay(2000);

        logger.LogInformation("Health check completed.");

        return Ok(
            new
            {
                Response = "Pong.",
                ServicesHealth = "Healthy.",
                ServerTime = DateTime.UtcNow
            });
    }
}