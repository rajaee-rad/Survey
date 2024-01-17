using System;
using CustomerSurveySystem.Services.Class;

namespace CustomerSurveySystem.Models
{
    public class Data
    {
        public Guid QuestionId { get; set; }
        public AnswerData Answer { get; set; }
    }
}