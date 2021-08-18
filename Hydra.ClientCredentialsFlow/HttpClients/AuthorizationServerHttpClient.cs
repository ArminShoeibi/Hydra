using Hydra.Shared;
using System.Text;

namespace Hydra.ClientCredentialsFlow.HttpClients;
public class AuthorizationServerHttpClient
{
    private readonly HttpClient _httpClient;

    public AuthorizationServerHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<TokenResponseDto> GetToken()
    {
        string path = "/connect/token";
        FormUrlEncodedContent clientCredentialsFlowFormContent = new(new List<KeyValuePair<string, string>>
        {
            new KeyValuePair<string,string>("grant_type","client_credentials"),
            new KeyValuePair<string,string>("scope","test-scope")
        });

        byte[] clientCredentialsAsBytes = Encoding.UTF8.GetBytes(StaticDetails.HydraClientCredentialsFlowClientId + ":" + StaticDetails.HydraClientCredentialsFlowSecret);
        string clientCredentialsAsBase64 = Convert.ToBase64String(clientCredentialsAsBytes);

        _httpClient.DefaultRequestHeaders.Add("Authorization", $"BASIC {clientCredentialsAsBase64}");

        HttpResponseMessage clientCredentialsResponse = await _httpClient.PostAsync(path, clientCredentialsFlowFormContent);
        return await clientCredentialsResponse.Content.ReadFromJsonAsync<TokenResponseDto>();
    }


}
