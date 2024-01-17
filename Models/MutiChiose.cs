using System.Collections.Generic;
using CustomerSurveySystem.Services.Interface;

namespace CustomerSurveySystem.Models
{
    public class MultiChoice : IAnswerData
    {
        public List<string> Value { get; set; }
        public string Description { get; set; }
    }
}