using System.Collections.Generic;
using CustomerSurveySystem.Services.Interface;
using Newtonsoft.Json;

namespace CustomerSurveySystem.Models
{
    public class SurveyQuestionData
    {
        [JsonProperty("$NetType")]
        public string NetType { get; set; }
        public int? MaxSelectable { get; set; }
        public List<string> Options { get; set; }
        public bool IsMultiSelect { get; set; }
    }

    public class SurveyQuestion
    {
        public SurveyQuestionData Data { get; set; }
    }
    
}