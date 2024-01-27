using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using CaptchaMvc.HtmlHelpers;
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
        [HttpPost]
        public async Task<ActionResult> Login(string nationalCode, string password)
        {
            if (!this.IsCaptchaValid("کپچا نامعتبر است."))
            {
                ViewBag.ErorrMessage = "کپچا نامعتبر است.";
                return RedirectToAction("Index", "Account");
            }
            var result =  await _service.Login(nationalCode, password);
            if (result)
            {
               FormsAuthentication.SetAuthCookie(nationalCode, false);
                return RedirectToAction("Index", "Questionnaire", Guid.Empty);
            }

            return RedirectToAction("Index", "Account");
        }
        [HttpPost]
        public async Task<string> Signup(string nationalCode)
        {
            var result =  await _service.Signup(nationalCode);
            return null;

        }

        [HttpPost]
        public Task<ActionResult> Logout()
        {
            FormsAuthentication.SignOut();
            return Task.FromResult<ActionResult>(RedirectToAction("Index", "Home"));

        }
    }
}