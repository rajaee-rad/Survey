using System;
using System.Collections.Generic;
using CustomerSurveySystem.Enums;
using CustomerSurveySystem.Services.Class;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CustomerSurveySystem.Models
{
    public interface IAnswerData
    {
    }

    public class Data
    {
        public Guid QuestionId { get; set; }
        public AnswerData Answer { get; set; }
    }

    public class AnswerData
    {
        [JsonConverter(typeof(NetTypeConverter),
            new object[]
                { new[] { typeof(Number), typeof(OneChoice), typeof(MultiChoice), typeof(Score), typeof(Text) } })]
        public IAnswerData Data { get; set; }
    }

    public class MultiChoice : IAnswerData
    {
        public List<string> Value { get; set; }
        public string Description { get; set; }
    }

    public class Text : IAnswerData
    {
        public string Value { get; set; }
    }

    public class Score : IAnswerData
    {
        public int Value { get; set; }
    }

    public class Number : IAnswerData
    {
        public int Value { get; set; }
    }

    public class OneChoice : AnswerData
    {
        public string Value { get; set; }
    }
}