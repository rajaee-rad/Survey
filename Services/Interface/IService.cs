using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerSurveySystem.Models;

namespace CustomerSurveySystem.Services.Interface
{
    public interface IService
    {
        Task<List<Questionnaire>> GetQuestionnairesOfWebsite();
    }
}