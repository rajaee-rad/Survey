using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CustomerSurveySystem.Enums
{
    public enum QuestionType
    {
        [Display(Name = "چند گزینه ای")] MultiChoice = 1,
        [Display(Name = "امتیازی")] Score = 2,
        [Display(Name = "تشریحی")] Text = 3,
        [Display(Name = "عددی")] Number = 4
    }
}