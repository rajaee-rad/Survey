using CustomerSurveySystem.Models;
using CustomerSurveySystem.Services.Interface;
using Newtonsoft.Json;

namespace CustomerSurveySystem.Services.Class
{
    public class AnswerData : IAnswerData
    {
        // [JsonConverter(typeof(NetTypeConverter),
        //     new object[]
        //     {
        //         new[] { typeof(Number), typeof(OneChoice), typeof(MultiChoice), typeof(Score), typeof(Text) }
        //     })]
        public IAnswerData Data { get; set; }
    }
}