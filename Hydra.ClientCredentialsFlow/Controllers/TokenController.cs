using Hydra.ClientCredentialsFlow.HttpClients;
using Microsoft.AspNetCore.Mvc;

namespace Hydra.ClientCredentialsFlow.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TokenController : ControllerBase
{
    private readonly AuthorizationServerHttpClient _authorizationServerHttpClient;

    public TokenController(AuthorizationServerHttpClient authorizationServerHttpClient)
    {
        _authorizationServerHttpClient = authorizationServerHttpClient;
    }

    [HttpGet]
    public async Task<IActionResult> GetToken()
    {
        var tokenResponseDto = await _authorizationServerHttpClient.GetToken();
        return Ok(tokenResponseDto);
    }
}
