using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using CustomerSurveySystem.Class;
using CustomerSurveySystem.Models;
using CustomerSurveySystem.Services.Interface;
using Newtonsoft.Json;
using Serilog;

namespace CustomerSurveySystem.Services.Class
{
    public class Service : IService
    {
        public async Task<List<Questionnaire>> GetQuestionnairesOfWebsite()
        {
            try

            {
                var param = new
                {
                    domainId = Guid.Parse(ConfigurationManager.AppSettings["WebSiteDomainId"])
                };
                var response = await ApiCaller.Call(QuestionnaireApiUrl.QuestionnairesByDomainId, param);
                if (!response.IsSuccessful || response.Content == null)
                {
                    return null;
                }

                var result = JsonConvert
                    .DeserializeObject<Response<Questionnaire>>(response.Content)
                    ?.Data.ToList();

                return result;
            }
            catch (Exception e)
            {
                Log.Logger.Fatal($"Error at GetQuestionnairesOfWebsite! {e.Message}");
            }

            return null;
        }
        
        
    }
}