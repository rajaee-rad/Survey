using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CustomerSurveySystem.Services.Interface;

namespace CustomerSurveySystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly IService _service;
        public HomeController(IService service)
        {
            _service = service;
        }
        public HomeController()
        {
            
        }
        public async Task<ActionResult> Index()
        {
            var questionnairesOfWebsite = await _service.GetQuestionnairesOfWebsite();
            foreach (var item in questionnairesOfWebsite)
            {
                var key = $"Questionnaire_{item.Id}";
                var encodeData = Convert.ToBase64String(Encoding.UTF8.GetBytes(item.Title));
                Response.Cookies.Add(new HttpCookie(key, encodeData));
            }
            return View(questionnairesOfWebsite.ToList());
        }
    }
}