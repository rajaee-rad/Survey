namespace CustomerSurveySystem.Class
{
    public static class QuestionnaireApiUrl
    {
        private const string Prefix = "/api/Questionnaire";
        private const string AnsweringService = "/AnsweringService";

        public const string GetDomains = Prefix + AnsweringService + "/GetDomains";
        public const string GetQuestionnairesByDomainId = Prefix + AnsweringService + "/GetQuestionnairesByDomainId";
        public const string GetQuestionnaireForPerson = Prefix + AnsweringService + "/GetQuestionnaireForPerson";
        public const string NextStep = Prefix + AnsweringService + "/NextStep";
        public const string GetPolls = Prefix + AnsweringService + "/GetPolls";
        public const string GetPollById = Prefix + AnsweringService + "/GetPollById";
        public const string GetPollQuestionsByPollId = Prefix + AnsweringService + "/GetPollQuestionsByPollId";
        public const string GetDomainIdByLocation = Prefix + AnsweringService + "/GetDomainIdByLocation";
        public const string GetAnswerSheetIdByQuestionnaireId = Prefix + AnsweringService + "/GetAnswerSheetIdByQuestionnaireId";
    }
}