using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using CustomerSurveySystem.Enums;
using CustomerSurveySystem.Models;
using CustomerSurveySystem.Services.Interface;
using Newtonsoft.Json;

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
        public async Task<ActionResult> Index(Guid questionnaireId, bool needLogin)
        {
            if (needLogin && !User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Account");
            }
            var dto = new NextStepRequestDto()
            {
                QuestionnaireId = questionnaireId
            };
            var result = await _service.NextStep(dto);
            foreach (var question in result)
            {
                if (question.QuestionType == QuestionType.MultiChoice)
                {
                    var multiChoiceData = JsonConvert.DeserializeObject<JsonData>(question.QuestionDetail.ToString());
                    //{"Data":{"$NetType":"MultiChoice","MaxSelectable":null,"Options":["بلی","خیر"],"IsMultiSelect":true}}
                    
                }
                
            }
            return View(result);
        }
    }
}