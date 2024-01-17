using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using CustomerSurveySystem.Models;
using CustomerSurveySystem.Services.Interface;

namespace CustomerSurveySystem.Controllers
{
    public class QuestionnaireController : Controller
    {
        private readonly IService _service;

        public QuestionnaireController()
        {
            
        }
        public QuestionnaireController(IService service)
        {
            _service = service;
        }
        public async Task<ActionResult> Index(Guid questionnaireId)
        {
            var dto = new NextStepRequestDto()
            {
                QuestionnaireId = questionnaireId
            };
            var result = await _service.NextStep(dto);
            
            return View(result);
        }
    }
}