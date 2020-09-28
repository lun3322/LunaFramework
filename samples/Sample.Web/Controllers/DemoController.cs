using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Luna.Application.Dto;
using Luna.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Sample.Core.SampleService;

namespace Sample.Web.Controllers
{
    [Route("api/demo")]
    public class DemoController : Controller
    {
        private readonly ISampleService _sampleService;

        public DemoController(ISampleService sampleService)
        {
            _sampleService = sampleService;
        }

        [HttpGet("success")]
        public ResponseVm Success()
        {
            return ResponseVm.Success("ok");
        }

        [HttpGet("error")]
        public ResponseVm Error()
        {
            throw new LunaUserFriendlyException("出错", 400);
        }

        [HttpPost("model")]
        public ResponseVm Model([FromBody] CustomModel model)
        {
            return ResponseVm.Success("ok");
        }

        [HttpGet("sample")]
        public ResponseVm Sample()
        {
            var message = _sampleService.GetMessage();
            return ResponseVm.Success(message);
        }
    }

    public class CustomModel
    {
        [DisplayName("序号")]
        [Required(ErrorMessage = "{0}是必填项")]
        public int? Id { get; set; }

        [DisplayName("姓名")]
        [Required(ErrorMessage = "{0}是必填项")]
        [StringLength(5, MinimumLength = 2, ErrorMessage = "{0}长度必须为{2}-{1}")]
        public string Name { get; set; }
    }
}