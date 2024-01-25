using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using CustomerSurveySystem.Enums;
using CustomerSurveySystem.Models;
using CustomerSurveySystem.Services.Class;
using CustomerSurveySystem.Services.Interface;
using Newtonsoft.Json;
using Microsoft.AspNet.Identity;

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

        [System.Web.Mvc.HttpPost]
        public async Task<ActionResult> Index(Guid questionnaireId, bool needLogin)
        {
            if (needLogin && !User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Account");
            }

            // var customer = await _service.RequestCustomer(User.Identity.GetUserId());
            //var answerSheetId = await _service.GetAnswerSheetIdByQuestionnaireId(questionnaireId, customer.Id.ToString());
            var answerSheetId = "D9A7FE2F-51AD-4F84-9D65-B0FD000CBA3A";
            var dto = new NextStepSendData()
            {
                QuestionnaireId = questionnaireId
            };
            var result = await _service.NextStep(dto);
            foreach (var question in result)
            {
                if (question.QuestionType != QuestionType.MultiChoice) continue;
                var multiChoiceData =
                    JsonConvert.DeserializeObject<SurveyQuestion>(question.QuestionDetail.ToString());
                question.Questions = new SurveyQuestionDetail()
                {
                    Options = multiChoiceData.QuestionDetail.Options,
                    MaxSelectable = multiChoiceData.QuestionDetail.MaxSelectable,
                    IsMultiSelect = multiChoiceData.QuestionDetail.IsMultiSelect,
                    NetType = multiChoiceData.QuestionDetail.NetType
                };
            }

            ViewData["QuestionnaireId"] = questionnaireId;
            ViewData["AnswerSheetId"] = answerSheetId;
            var questionnaireTitle =
                Encoding.UTF8.GetString(
                    Convert.FromBase64String(Request.Cookies[$"Questionnaire_{questionnaireId}"]?.Value));
            ViewData["QuestionnaireTitle"] = questionnaireTitle;
            return View(result);
        }

        [System.Web.Mvc.HttpPost]
        public async Task<ActionResult> NextStep(Guid? answerSheetId, Guid? currentStepId, Guid? questionnaireId,
             IList<AnswerOfQuestion> answerData)
        {
         
            var dto = new NextStepSendData()
            {
                QuestionnaireId = questionnaireId ?? (Guid.Empty),
                AnswerSheetId = answerSheetId ?? (Guid.Empty),
                CurrentStepId = currentStepId ?? (Guid.Empty),
                Answers =  answerData
            };
           var result = await  _service.NextStep(dto);
            //return  RedirectToAction("NextStep", questionnaireId)
            return null;
        }
    }
}