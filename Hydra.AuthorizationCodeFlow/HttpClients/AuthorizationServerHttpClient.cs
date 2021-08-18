using Hydra.Shared;
using System.Text;

namespace Hydra.AuthorizationCodeFlow.HttpClients;
public class AuthorizationServerHttpClient
{
    private readonly HttpClient _httpClient;

    public AuthorizationServerHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }


    public async Task<TokenResponseDto> GetToken(string code,string redirectUri)
    {
        string path = "/connect/token";
        FormUrlEncodedContent clientCredentialsFlowFormContent = new(new List<KeyValuePair<string, string>>
        {
            new KeyValuePair<string,string>("grant_type","authorization_code"),
            new KeyValuePair<string,string>("code",code),
            new KeyValuePair<string,string>("client_id",StaticDetails.HydraAuthorizationCodeFlowClientId),
            new KeyValuePair<string,string>("client_secret",StaticDetails.HydraAuthorizationCodeFlowSecret),
            new KeyValuePair<string,string>("redirect_uri",redirectUri),
        });

        HttpResponseMessage clientCredentialsResponse = await _httpClient.PostAsync(path, clientCredentialsFlowFormContent);
        return await clientCredentialsResponse.Content.ReadFromJsonAsync<TokenResponseDto>();
    }


}
