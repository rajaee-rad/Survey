using System;
using System.Configuration;
using System.Globalization;
using System.Runtime.Caching;
using System.Threading.Tasks;
using CustomerSurveySystem.Models;
using Newtonsoft.Json;
using RestSharp;
using Serilog;

namespace CustomerSurveySystem.Class
{
    public static class QuestionnaireAuthorize 
    {
                    
        private const string TokenCacheKey = "TokenCacheKey";

        public static async Task<string> GetToken()
        {
            try
            {

                // var memoryCache = MemoryCache.Default;
                // if (memoryCache.Contains(TokenCacheKey))
                // {
                //     memoryCache.Remove()
                //     //return memoryCache.Get(TokenCacheKey)?.ToString();
                // }


                var client = new RestClient(ConfigurationManager.AppSettings["QuestionnaireHost"]);
                var request = new RestRequest("/token?", Method.Post);

                request.AddParameter("username", ConfigurationManager.AppSettings["QuestionnaireUsername"]);
                request.AddParameter("password", ConfigurationManager.AppSettings["QuestionnairePassword"]);
                request.AddParameter("grant_type", "password");

                var response = client.Execute(request);

                if (!response.IsSuccessful)
                {
                    Log.Logger.Information(response.Content);
                }

                var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(response.Content);
                var cacheItemPolicy = new CacheItemPolicy()
                {
                    AbsoluteExpiration = DateTime.ParseExact(tokenResponse.Expires, "ddd, dd MMM yyyy HH:mm:ss 'GMT'",
                        CultureInfo.InvariantCulture),
                };

                //memoryCache.Add(TokenCacheKey, tokenResponse.AccessToken, cacheItemPolicy);
                return tokenResponse.AccessToken;

            }
            catch (Exception e)
            {
                Log.Logger.Fatal(e.Message);
            }

            return string.Empty;
        }
    }
}