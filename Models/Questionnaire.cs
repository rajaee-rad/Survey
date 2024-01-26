using System;
using System.Text.Json.Serialization;
using CustomerSurveySystem.Enums;

namespace CustomerSurveySystem.Models
{
    public class Questionnaire
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string Description { get; set; }
        
        public QuestionnaireState State { get; set; }
        public bool NeedLogin { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifiedDate { get; set; }
    }

    
}