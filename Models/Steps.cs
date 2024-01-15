using System;

namespace CustomerSurveySystem.Models
{
    public class Steps
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public double OrderNumber { get; set; }
        public Guid QuestionnaireId { get; set; }
        public Guid? NextStepId { get; set; }
    }
}