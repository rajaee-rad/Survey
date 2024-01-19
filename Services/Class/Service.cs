using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Results;
using CustomerSurveySystem.Class;
using CustomerSurveySystem.Models;
using CustomerSurveySystem.Services.Interface;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using Serilog;

namespace CustomerSurveySystem.Services.Class
{
    public class Service : IService
    {
        public async Task<IList<Questionnaire>> GetQuestionnairesOfWebsite()
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
                    ?.Data;

                return result;
            }
            catch (Exception e)
            {
                Log.Logger.Fatal($"Error at GetQuestionnairesOfWebsite! {e.Message}");
            }

            return null;
        }

        public async Task<IList<QuestionDto>> NextStep(NextStepRequestDto requestDto)
        {
            try
            {
                var param = new
                {
                    answerSheetId = requestDto.AnswerSheetId,
                    currentStepId = requestDto.CurrentStepId,
                    questionnaireId = requestDto.QuestionnaireId,
                    answerList = requestDto.AnswerList
                };
                var response = await ApiCaller.Call(QuestionnaireApiUrl.NextStep, param);
                if (!response.IsSuccessful || response.Content == null)
                {
                    return null;
                }

                var result = JsonConvert.DeserializeObject<Response<QuestionDto>>(response.Content)?.Data;
                return result;
            }
            catch (Exception e)
            {
                Log.Logger.Fatal($"Error at NextStep! {e.Message}");
            }

            return null;
        }

        public async Task<bool> Signup(string nationalCode)
        {
            try
            {
                var param = new
                {
                    nationalCode = nationalCode
                };
                var result = ApiCaller.Call(QuestionnaireApiUrl.Signup, param).Result;
                if (result.IsSuccessful != true && result.Content == null)
                {
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {
                Log.Logger.Fatal($"Error at Signup! {e.Message}");
            }

            return "";
        }

        public async Task<bool> Login(string nationalCode, string password)
        {
            try
            {
                var param = new
                {
                    nationalCode = nationalCode,
                    password = password
                };
                var response = await ApiCaller.Call(QuestionnaireApiUrl.Login, param);
                if (!response.IsSuccessful && response.Content == null)
                {
                    return false;
                }

                var result = JsonConvert.DeserializeObject<AuthenticationResponse>(response.Content).Data;
                return result;
            }
            catch (Exception e)
            {
                Log.Logger.Fatal($"Error at Login! {e.Message}");
            }

            return false;
        }
    }
}