using System;

namespace CustomerSurveySystem.Models
{
    public class Questionnaire
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid? DomainId { get; set; }
        public int State { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Description { get; set; }
        public bool NeedLogin { get; set; }
    }
}