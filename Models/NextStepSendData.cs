using System;
using System.Collections.Generic;

namespace CustomerSurveySystem.Models
{
    public class NextStepSendData
    {
        public Guid AnswerSheetId { get; set; }
        public Guid? CurrentStepId { get; set; }
        public Guid QuestionnaireId { get; set; }
        public IList<AnswerWithSerializedData> AnswerList { get; set; }
    }
}