using System.Linq;
using System.Threading.Tasks;
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
            return View(questionnairesOfWebsite.ToList());
        }
    }
}