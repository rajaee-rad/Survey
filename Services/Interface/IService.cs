using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerSurveySystem.Models;
using Newtonsoft.Json;

namespace CustomerSurveySystem.Services.Interface
{
    public interface IService
    {
        Task<IList<Questionnaire>> GetQuestionnairesOfWebsite();

        Task<IList<QuestionDto>> NextStep(NextStepSendData sendData);
        Task<CustomerDto> RequestCustomer(string nationalCode);
        Task<Guid> GetAnswerSheetIdByQuestionnaireId(Guid questionnaireId, string customerId);
        Task<bool> Signup(string nationalCode);
        Task<bool> Login(string nationalCode, string password);
    }
}