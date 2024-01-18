using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using CustomerSurveySystem.Services.Interface;

namespace CustomerSurveySystem.Controllers
{
    public class LoginController : Controller
    {
        private readonly IService _service;

        public LoginController(IService service)
        {
            _service = service;
        }
        // GET
        public ActionResult Index()
        {
           return View();
        }

        public async Task<string> Login(string nationalCode, string password)
        {
            var result =  await _service.Login(nationalCode, password);
            if (result)
            {
                return "ok";
            }
            else
            {
                return "error";
            }
        }

        public async Task<string> Signup(string nationalCode)
        {
            return await _service.Signup(nationalCode);
        }
    }
}