using System;
using Luna.Application.Dto;
using Luna.Web.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace DemoApp.WebDemo.Controllers
{
    public class ExceptionTestController : LunaController
    {
        public IActionResult Index()
        {
            return View();
        }

        public ViewResult Test()
        {
            throw new Exception("Test exception");
        }

        public ViewResult ViewTest()
        {
            return View();
        }

        [HttpGet]
        public ResponseVm GetVm()
        {
            Logger.Info("GetVm exception");
            throw new Exception("GetVm exception");
        }

        [HttpPost]
        public ResponseVm PostVm()
        {
            throw new Exception("PostVm exception");
        }
    }
}