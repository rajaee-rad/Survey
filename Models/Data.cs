using System;
using CustomerSurveySystem.Services.Class;
using Newtonsoft.Json;

namespace CustomerSurveySystem.Models
{
    public class Data
    {
        public Guid QuestionId { get; set; }
        [JsonConverter(typeof(string), new object[] { new[] { typeof(Number), typeof(OneChoice), typeof(SurveyQuestionData), typeof(Score), typeof(Text) } })]
        public AnswerData Answer { get; set; }
    }
}