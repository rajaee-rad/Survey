using System.Collections.Generic;

namespace CustomerSurveySystem.Models
{
    /// <summary>
     /// برای دریافت پاسخ سوالات نظرسنجی از کاربر پاسخ دهنده
    /// </summary>
    public class Answer
    {
        public string Value { get; set; }
        public string Description  { get; set; }
    }
}