using System;

namespace CustomerSurveySystem.Models
{
    public class Rules
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public string JsonCondition { get; set; }
        public Guid StepId { get; set; }
        public Guid NextStepId { get; set; }
    }
}