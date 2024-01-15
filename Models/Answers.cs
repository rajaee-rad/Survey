using System;

namespace CustomerSurveySystem.Models
{
    public class Answers
    {
        public Guid Id { get; set; }
        public Guid AnswerSheetId { get; set; }
        public Guid QuestionId { get; set; }
        public string JsonData { get; set; }
    }
}