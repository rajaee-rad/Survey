using System.Configuration;

namespace CustomerSurveySystem.Class
{
    public static class QuestionnaireApiUrl
    {
        private const string Prefix = "api/Questionnaire/AnsweringService";

        public const string GetDomains = Prefix  + "/GetDomains";
        public const string QuestionnairesByDomainId = Prefix  + "/GetQuestionnairesByDomainId";
        public const string GetQuestionnaireForPerson = Prefix  + "/GetQuestionnaireForPerson";
        public const string NextStep = Prefix  + "/NextStep";
        public const string Login = "servicePath/CustomerService/Login";
        public const string Signup = "servicePath/CustomerService/Signup";
        public const string GetPolls = Prefix  + "/GetPolls";
        public const string GetPollById = Prefix  + "/GetPollById";
        public const string GetPollQuestionsByPollId = Prefix  + "/GetPollQuestionsByPollId";
        public const string GetDomainIdByLocation = Prefix  + "/GetDomainIdByLocation";
        public const string GetAnswerSheetIdByQuestionnaireId = Prefix  + "/GetAnswerSheetIdByQuestionnaireId";
    }
}