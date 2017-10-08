using System;
using System.Collections.Generic;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Calendar.v3;
using OAuth2.Configuration;
using OAuth2.Infrastructure;
using System.Net.Http;
using System.Threading.Tasks;
using antlr;
using Newtonsoft.Json;

namespace StruggleApplication.framework
{
    public class OAuthoriser
       
    { 
        private static readonly String clientID = "411401250926-e5gu2e7ff7ickohmnv06vcjiv7dtio54.apps.googleusercontent.com";
        private static readonly String clientSecret = "XqCJM1CAX_eWPp00HNrT5g7Y";
        private static readonly String redirectUrl = "http://localhost";
     
        public static String GenerateLink()
        {
            IClientConfiguration config = new ClientConfiguration
            {
                IsEnabled = true,
                ClientId = clientID,
                ClientSecret = clientSecret,
                RedirectUri = redirectUrl,
                Scope = CalendarService.Scope.Calendar
            };
            OAuth2.Client.Impl.GoogleClient client = new OAuth2.Client.Impl.GoogleClient(
                new RequestFactory(), config);
            return client.GetLoginLinkUri();
        }

        public async static Task<TokenResponse> GenerateTokens(string code)
        {
            //TODO delete
            Console.WriteLine("We are in the Token-Generator method");
            HttpClient client = new HttpClient();
                
            var values = new Dictionary<string, string>
            {
                { "code", code },
                { "client_id", clientID },
                {"client_secret", clientSecret},
                {"redirect_uri", redirectUrl},
                {"grant_type", "authorization_code"}
            };
            var content = new FormUrlEncodedContent(values);
            try
            {
                var response = await client.PostAsync("https://www.googleapis.com/oauth2/v4/token", content);
                Console.WriteLine(response.StatusCode);
                var responseString = await response.Content.ReadAsStringAsync();
                var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseString);

                TokenResponse token = new TokenResponse();    
                token.AccessToken = dict.GetValueOrDefault("access_token");
                token.RefreshToken = dict.GetValueOrDefault("refresh_token");
                token.ExpiresInSeconds = long.Parse(dict.GetValueOrDefault("expires_in"));
                token.TokenType = dict.GetValueOrDefault("token_type");
                Console.WriteLine(token.AccessToken);

                return token;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return null;
        }
    }
}