using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        [HttpGet]
        public ResponseVm GetVm()
        {
            throw new Exception("GetVm exception");
        }

        [HttpPost]
        public ResponseVm PostVm()
        {
            throw new Exception("PostVm exception");
        }
    }
}