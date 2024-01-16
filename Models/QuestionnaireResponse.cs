using System.Collections.Generic;

namespace CustomerSurveySystem.Models
{
    public class Response<T>
    {
        public bool Success { get; set; }
        public IList<T> Data { get; set; }
        public string Message { get; set; }
        public string MessageCode { get; set; }
        public IList<string> MessageDetails { get; set; }
        public string MessageTitle { get; set; }
    }
}