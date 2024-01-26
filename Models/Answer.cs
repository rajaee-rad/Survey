using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace CustomerSurveySystem.Models
{
    public class Answer<T>
    {
        public Guid QuestionId { get; set; }
        [JsonPropertyName("Answer")]
        public DataAnswer<T> AnswerOfQuestionData { get; set; }
    }

    public class MultiChoice
    {
        [JsonPropertyName("$NetType")] public string NetType { get; set; }
        [JsonPropertyName("Value")] public List<string> Value { get; set; }
    }

    public class Score
    {
        [JsonPropertyName("$NetType")] public string NetType { get; set; }

        [JsonPropertyName("Value")] public int Value { get; set; }
    }

    public class Text
    {
        [JsonPropertyName("$NetType")] public string NetType { get; set; }

        [JsonPropertyName("Value")] public Text Value { get; set; }
    }

    public class DataAnswer<T>
    {
        [JsonPropertyName("Data")] public T Data { get; set; }
    }
}