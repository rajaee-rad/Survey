using System;
using System.Collections.Generic;

namespace CustomerSurveySystem.Models
{
    public class NextStepRequestDto
    {
        public Guid AnswerSheetId { get; set; }
        public Guid? CurrentStepId { get; set; }
        public Guid QuestionnaireId { get; set; }
        public IEnumerable<SendAnswerDataDto> AnswerList { get; set; }
    }
}