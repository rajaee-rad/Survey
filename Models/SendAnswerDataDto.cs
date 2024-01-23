using System;

namespace CustomerSurveySystem.Models
{
    /// <summary>
    /// برای ارسال پاسخ سوالات نظرسنجی به سرویس Questionnaire
    /// </summary>
    public class AnswerWithSerializedData
    {
        public Guid QuestionId { get; set; }
        public string Answer { get; set; }
    }
}