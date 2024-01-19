using System.Configuration;
using System.Threading.Tasks;
using RestSharp;

namespace CustomerSurveySystem.Class
{
    public static class ApiCaller
    {
        private static readonly string Host = ConfigurationManager.AppSettings["QuestionnaireHost"];

        public static async Task<RestResponse> Call(string url, object parameter = null)
        {
            var client = new RestClient(Host);
            var request = new RestRequest(url, Method.Post);
            if (parameter != null)
            {
                request.AddJsonBody(parameter);
                request.RequestFormat = DataFormat.Json;

            }

            request.AddHeader("Authorization", "Bearer " + await QuestionnaireAuthorize.GetToken());
            return await client.ExecuteAsync(request);
        }
    }
}