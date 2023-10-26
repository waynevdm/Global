using IdentityModel.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartTests.Helpers
{
    public static class AuthHelper
    {
        public static async Task<HttpClient> GetAuthedClient()
        {
            var httpClient = new HttpClient();
            var passwordTokenReq = new PasswordTokenRequest
            {
                Address = "http://localhost:5000/connect/token",
                GrantType = "password",
                ClientId = "roclient",
                ClientSecret = "secret",

                UserName = "alice",
                Password = "alice"
            };
            var identityServerResponse = await httpClient.RequestPasswordTokenAsync(passwordTokenReq);

            // Another HttpClient for talking now with our Protected API
            var client = new HttpClient();

            // 3. Set the access_token in the request Authorization: Bearer <token>
            client.SetBearerToken(identityServerResponse.AccessToken);
            return client;
       }
    }
}
