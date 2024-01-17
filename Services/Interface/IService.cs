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

        Task<IList<QuestionDto>> NextStep(NextStepRequestDto requestDto);
    }
}