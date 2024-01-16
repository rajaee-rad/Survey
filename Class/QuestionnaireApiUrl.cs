namespace CustomerSurveySystem.Class
{
    public static class QuestionnaireApiUrl
    {
        private const string Prefix = "/api/crm/QuestionnaireService";

        public const string GetDomains = Prefix  + "/GetDomains";
        public const string QuestionnairesByDomainId = Prefix  + "/QuestionnairesByDomainId";
        public const string GetQuestionnaireForPerson = Prefix  + "/GetQuestionnaireForPerson";
        public const string NextStep = Prefix  + "/NextStep";
        public const string GetPolls = Prefix  + "/GetPolls";
        public const string GetPollById = Prefix  + "/GetPollById";
        public const string GetPollQuestionsByPollId = Prefix  + "/GetPollQuestionsByPollId";
        public const string GetDomainIdByLocation = Prefix  + "/GetDomainIdByLocation";
        public const string GetAnswerSheetIdByQuestionnaireId = Prefix  + "/GetAnswerSheetIdByQuestionnaireId";
    }
}