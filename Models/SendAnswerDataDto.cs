using System;

namespace CustomerSurveySystem.Models
{
    public class SendAnswerDataDto
    {
        public Guid QuestionId { get; set; }
        public string Answer { get; set; }
    }
}