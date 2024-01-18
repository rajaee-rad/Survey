using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using CustomerSurveySystem.Services.Interface;

namespace CustomerSurveySystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly IService _service;

        public AccountController(IService service)
        {
            _service = service;
        }
        // GET
        public ActionResult Index()
        {
           return View();
        }
        [System.Web.Mvc.HttpPost]
        public async Task<ActionResult> Login(string nationalCode, string password)
        {
            var result =  await _service.Login(nationalCode, password);
            if (result)
            {
               FormsAuthentication.SetAuthCookie(nationalCode, false);
                return RedirectToAction("Index", "Questionnaire", Guid.Empty);
            }
            else
            {
                //return View();
                return null;
            }
        }
        [System.Web.Mvc.HttpPost]
        public async Task<string> Signup(string nationalCode)
        {
            var result =  await _service.Signup(nationalCode);
            return null;

        }

        [System.Web.Http.HttpPost]
        public async Task<ActionResult> Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");

        }
    }
}