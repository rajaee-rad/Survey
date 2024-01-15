using System;

namespace CustomerSurveySystem.Models
{
    public class AnswerSheets
    {
        public Guid Id { get; set; }
        public string PersonName { get; set; }
        public Guid QuestionnaireId { get; set; }
        public string PersonId { get; set; }
        public string QuestionerId { get; set; }
        public string QuestionerName { get; set; }
        public bool Complete { get; set; }
        public string FollowingCode { get; set; }
        public long? CustomerNumber { get; set; }
        public string BranchCreatorCustomerCode { get; set; }
        public string BranchCreatorCustomerName { get; set; }
    }
}