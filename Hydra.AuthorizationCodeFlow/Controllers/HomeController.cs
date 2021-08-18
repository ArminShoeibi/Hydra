using Hydra.AuthorizationCodeFlow.HttpClients;
using Hydra.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Hydra.AuthorizationCodeFlow.Controllers;
public class HomeController : Controller
{
    private readonly AuthorizationServerHttpClient _authorizationServerHttpClient;

    public HomeController(AuthorizationServerHttpClient authorizationServerHttpClient)
    {
        _authorizationServerHttpClient = authorizationServerHttpClient;
    }

    public IActionResult Index()
    {

        string redirectUri = Url.ActionLink(nameof(GetToken), "Home", null, Request.Scheme);
        string responseType = "code";
        string clientId = StaticDetails.HydraAuthorizationCodeFlowClientId;
        string scope = "test-scope";


        string url = $"https://localhost:5001/connect/authorize?response_type={responseType}&client_id={clientId}&scope={scope}&redirect_uri={redirectUri}";
        return Redirect(url);
    }
    public async Task<IActionResult> GetToken(string code, string state)
    {
        string redirectUri = Url.ActionLink(nameof(GetToken), "Home", null, Request.Scheme);
        TokenResponseDto tokenResponseDto = await _authorizationServerHttpClient.GetToken(code, redirectUri);
        return Json(tokenResponseDto);
    }
}
