using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using CustomerSurveySystem.Class;
using CustomerSurveySystem.Models;
using Newtonsoft.Json;
using Serilog;

namespace CustomerSurveySystem.Controllers
{
    public class QuestionnaireController : ApiController
    {
        public async Task<List<Domains>> Domains()
        {
            try

            {
                var response = await ApiCaller.Call(QuestionnaireApiUrl.GetDomains, null);
                if (response.IsSuccessful || response.Content == null)
                {
                    return null;
                }
                var result = JsonConvert
                    .DeserializeObject<QuestionnaireResponse<Domains>>(response.Content)
                    ?.Data.ToList();

                return result;
            }
            catch (Exception e)
            {
                Log.Logger.Fatal($"Error at Cities! {e.Message}");
            }

            return null;
        }
    }
}
