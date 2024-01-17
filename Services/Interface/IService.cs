using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerSurveySystem.Models;
using Newtonsoft.Json;

namespace CustomerSurveySystem.Services.Interface
{
    public interface IService
    {
        Task<List<Questionnaire>> GetQuestionnairesOfWebsite();

        List<QuestionDto> NextStep(Guid answerSheetId, Guid? currentStepId, Guid questionnaireId,
            IEnumerable<Data> answerList);
    }

    
   

   

   
  

   
    

   
   
}