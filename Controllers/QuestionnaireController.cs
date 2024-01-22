using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        [HttpPost]
        public async Task<ActionResult> Index(Guid questionnaireId, bool needLogin)
        {
            if (needLogin && !User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Account");
            }

            // var customer = await _service.RequestCustomer(User.Identity.GetUserId());
            //var answerSheetId = await _service.GetAnswerSheetIdByQuestionnaireId(questionnaireId, customer.Id.ToString());
            var answerSheetId = "D9A7FE2F-51AD-4F84-9D65-B0FD000CBA3A";
            var dto = new NextStepRequestDto()
            {
                QuestionnaireId = questionnaireId
            };
            var result = await _service.NextStep(dto);
            foreach (var question in result)
            {
                if (question.QuestionType != QuestionType.MultiChoice) continue;
                var multiChoiceData =
                    JsonConvert.DeserializeObject<SurveyQuestion>(question.QuestionDetail.ToString());
                question.Questions = new SurveyQuestionData()
                {
                    Options = multiChoiceData.Data.Options,
                    MaxSelectable = multiChoiceData.Data.MaxSelectable,
                    IsMultiSelect = multiChoiceData.Data.IsMultiSelect,
                    NetType = multiChoiceData.Data.NetType
                };
            }

            ViewData["QuestionnaireId"] = questionnaireId;
            ViewData["AnswerSheetId"] = answerSheetId;
            var questionnaireTitle = Encoding.UTF8.GetString(Convert.FromBase64String(Request.Cookies[$"Questionnaire_{questionnaireId}"]?.Value));
            ViewData["QuestionnaireTitle"] = questionnaireTitle;
            return View(result.OrderBy(x=>x.OrderNumber).ToList());
        }

        [HttpPost]
        public async Task<ActionResult> NextStep(Guid? answerSheetId, Guid? currentStepId, Guid? questionnaireId,
            List<string> radio, List<string> checkbox, List<string> texts, List<string> numbers)
        {
            List<SendAnswerDataDto> data = new List<SendAnswerDataDto>();
            string value = "\"Data\":{\"Value\":[";
            //{"Data":{"Value":["نمیدانم","باید توضیح بدهم"],"Description":"تست","$NetType":"MultiChoice"}}
            if (radio.Any())
            {
                var i = 0;
                foreach (var item in radio)
                {
                    var sendAnswerDataDto = new SendAnswerDataDto();
                    sendAnswerDataDto.QuestionId = Guid.Parse(item.Split('_')[0]);
                    value += item.Split('_')[1];
                    data.Add(sendAnswerDataDto);
                    if (radio.Count >= i)
                    {
                        value += "\"Description\":\"تست\",\"$NetType\":\"MultiChoice\"}}";
                        sendAnswerDataDto.Answer = JsonConvert.SerializeObject(value);
                    }
                    else
                    {
                        i++;
                    }
                }
            }

            var nextStepRequestDto = new NextStepRequestDto()
            {
                QuestionnaireId = questionnaireId.Value,
                AnswerSheetId = answerSheetId.Value,
                AnswerList = data
            };
            var result = await _service.NextStep(nextStepRequestDto);
            return null;
        }
    }
}