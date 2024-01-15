using System;

namespace CustomerSurveySystem.Models
{
    public class Questions
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public double OrderNumber { get; set; }
        public Guid StepId { get; set; }
        public bool IsMandatory { get; set; }
        public string JsonDefinition { get; set; }
        public int QuestionType { get; set; }
        public bool SingleSelect { get; set; }
        public bool NeedDescription { get; set; }
    }
}