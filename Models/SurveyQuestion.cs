using System.Collections.Generic;
using CustomerSurveySystem.Services.Class;
using CustomerSurveySystem.Services.Interface;
using Newtonsoft.Json;

namespace CustomerSurveySystem.Models
{
    public class SurveyQuestionDetail
    {
        [JsonProperty("$NetType")]
        public string NetType { get; set; }
        public int? MaxSelectable { get; set; }
        public IList<string> Options { get; set; }
        public bool IsMultiSelect { get; set; }

    }

    public class SurveyQuestion
    {
        [JsonProperty("Data")]
        public SurveyQuestionDetail QuestionDetail { get; set; }

    }
    
}