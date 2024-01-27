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
        public async Task<ActionResult> Index(Guid questionnaireId, bool? needLogin)
        {
            if (needLogin.HasValue && needLogin.Value && !User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Account");
            }

            // var customer = await _service.RequestCustomer(User.Identity.GetUserId());
            //var answerSheetId = await _service.GetAnswerSheetIdByQuestionnaireId(questionnaireId, customer.Id.ToString());
            ViewData["AnswerSheetId"] = Guid.Parse("D9A7FE2F-51AD-4F84-9D65-B0FD000CBA3A");
            ViewData["QuestionnaireId"] = questionnaireId;

            List<QuestionDto> result;

            var dto = new NextStepSendData()
            {
                QuestionnaireId = questionnaireId
            };
            result = await _service.NextStep(dto) as List<QuestionDto>;


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


            return View(result);
        }

        [System.Web.Mvc.HttpPost]
        public async Task<ActionResult> NextStep(Guid? answerSheetId, Guid? currentStepId, Guid? questionnaireId,
            IList<AnswerOfQuestion> answerData)
        {
            try
            {
                var answersList = new List<Data>();
                var answers = answerData.Where(x => x.QuestionId != Guid.Empty).ToList();
                foreach (var item in answers)
                {
                    var dto = new Data
                    {
                        QuestionId = item.QuestionId,
                        Answer = new AnswerData()
                    };
                    switch (item.QuestionType)
                    {
                        case QuestionType.Number:
                            dto.Answer.Data = new Score()
                            {
                                Value = int.Parse(item.Answer.First())
                            };
                            break;
                        case QuestionType.Text:
                            dto.Answer.Data = new Text()
                            {
                                Value = item.Answer.First()
                            };
                            break;
                        case QuestionType.Score:
                            dto.Answer.Data = new Score()
                            {
                                Value = int.Parse(item.Answer.First())
                            };
                            break;
                        case QuestionType.MultiChoice:
                            dto.Answer.Data = new MultiChoice()
                            {
                                Value = item.Answer,
                                Description = item.Description
                            };

                            break;
                        default:
                            dto.Answer.Data = new MultiChoice()
                            {
                                Value = item.Answer
                            };
                            break;
                    }

                    answersList.Add(dto);
                }

                var sendAnswerDto = new NextStepSendData()
                {
                    QuestionnaireId = (Guid)questionnaireId,
                    AnswerSheetId = answerSheetId ?? (Guid.Empty),
                    CurrentStepId = currentStepId ?? (Guid.Empty),
                    Answers = answersList
                };

                var result = await _service.NextStep(sendAnswerDto);
                if (result != null && result.Any())
                {
                    return RedirectToAction("Index", "Questionnaire", new
                    {
                        questionnaireId = questionnaireId.Value,
                        answerSheetId = answerSheetId.Value
                    });
                }
            }
            catch (Exception ex)
            {
                return null;
            }

            return null;
        }
    }
}