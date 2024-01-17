using System;
using CustomerSurveySystem.Enums;

namespace CustomerSurveySystem.Services.Interface
{
    public class QuestionDto
    {
        public Guid CurrentStepId { get; set; }
        public Guid QuestionId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double OrderNumber { get; set; }
        public QuestionType QuestionType { get; set; }
        public bool SingleSelect { get; set; }
        public bool NeedDescription { get; set; }
        public bool IsRequire { get; set; }
        public object QuestionDetail { get; set; }
        public bool Finished { get; set; }
        
    }
}