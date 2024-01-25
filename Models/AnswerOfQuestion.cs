using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CustomerSurveySystem.Models
{
    /// <summary>
     /// برای دریافت پاسخ سوالات نظرسنجی از کاربر پاسخ دهنده
    /// </summary>
    public class AnswerOfQuestion
    {
        public Guid QuestionId { get; set; }
        public List<string> Answer { get; set; }
        public string Description  { get; set; }
    }
}