using System;
using System.Collections.Generic;

namespace CustomerSurveySystem.Models
{
    public class QuestionnaireViewModel
    {
        public Guid QuestionnaireId { get; set; }
        public string QuestionnaireTitle { get; set; }
        public Guid AnswerSheetId { get; set; }
        public IList<QuestionDto> Questions { get; set; }
    }
}