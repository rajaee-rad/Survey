using System;

namespace CustomerSurveySystem.Models
{
    public class CustomerDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string NationalCode { get; set; }
        public long? CustomerNumber { get; set; }
    }
}