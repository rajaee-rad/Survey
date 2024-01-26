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

        public async Task<IList<QuestionDto>> NextStep(NextStepSendData sendData)
        {
            try
            {
                var param = new
                {
                    answerSheetId = sendData.AnswerSheetId,
                    currentStepId = sendData.CurrentStepId,
                    questionnaireId = sendData.QuestionnaireId,
                    answerList =  JsonConvert.SerializeObject(sendData.Answers)
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


        public async Task<CustomerDto> RequestCustomer(string nationalCode)
        {
            try
            {
                var param = new
                {
                    nationalCode = nationalCode,
                };
                var response = await ApiCaller.Call(QuestionnaireApiUrl.RequestCustomer, param);
                if (!response.IsSuccessful || response.Content == null)
                {
                    return null;
                }

                var result = JsonConvert.DeserializeObject<Response<CustomerDto>>(response.Content)?.Data;
                return result.First();
            }
            catch (Exception e)
            {
                Log.Logger.Fatal($"Error at RequestCustomer! {e.Message}");
            }

            return null;
        }

        public async Task<Guid> GetAnswerSheetIdByQuestionnaireId(Guid questionnaireId, string customerId)
        {
            try
            {
                var param = new
                {
                    questionnaireId = questionnaireId,
                    customerId = customerId,
                };
                var response = await ApiCaller.Call(QuestionnaireApiUrl.GetAnswerSheetIdByQuestionnaireId, param);
                if (!response.IsSuccessful || response.Content == null)
                {
                    return Guid.Empty;
                }

                var result = JsonConvert.DeserializeObject<Response<Guid>>(response.Content)?.Data;
                return result.First();
            }
            catch (Exception e)
            {
                Log.Logger.Fatal($"Error at GetAnswerSheetIdByQuestionnaireId! {e.Message}");
            }

            return Guid.Empty;
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

            return true;
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