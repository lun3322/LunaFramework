using System.Diagnostics;
using DemoApp.Service;
using DemoApp.WebDemo.Models;
using Luna.Web.Mvc;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace DemoApp.WebDemo.Controllers
{
    public class HomeController : LunaController
    {
        private readonly IDemoService _demoService;

        public HomeController(IDemoService demoService)
        {
            _demoService = demoService;
        }

        public IActionResult Index()
        {
            var message = _demoService.GetMessage();
            ViewData["Message"] = message;
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            Logger.Error(exceptionHandlerPathFeature.Error.ToString());
            return Ok();
        }
    }
}