using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Luna.Application.Dto;
using Luna.Web.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoApp.WebDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : LunaController
    {
        [HttpGet]
        public ResponseVm Test([FromQuery] MessageModel vm)
        {
            return ResponseVm.Success(vm.Message);
        }
    }

    public class MessageModel
    {
        /// <summary>
        /// message
        /// </summary>
        [DisplayName("消息")]
        [StringLength(6, MinimumLength = 2, ErrorMessage = "{0}长度必须为{2}-{1}")]
        public string Message { get; set; }
    }
}