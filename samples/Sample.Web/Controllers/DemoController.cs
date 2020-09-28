using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Luna.Application.Dto;
using Luna.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Sample.Core.SampleService;

namespace Sample.Web.Controllers
{
    [Route("api/demo")]
    public class DemoController : Controller
    {
        private readonly IRedisService _redisService;
        private readonly ISampleService _sampleService;

        public DemoController(ISampleService sampleService, IRedisService redisService)
        {
            _sampleService = sampleService;
            _redisService = redisService;
        }

        [HttpGet("success")]
        public ResponseVm Success()
        {
            return ResponseVm.Success();
        }

        [HttpGet("error")]
        public ResponseVm Error()
        {
            throw new LunaUserFriendlyException("出错", 400);
        }

        [HttpPost("model")]
        public ResponseVm Model([FromBody] CustomModel model)
        {
            return ResponseVm.Success();
        }

        [HttpGet("sample")]
        public ResponseVm Sample()
        {
            var message = _sampleService.GetMessage();
            return ResponseVm.Success(message);
        }

        [HttpGet("redis1")]
        public async Task<ResponseVm> Redis1()
        {
            var message = await _redisService.GetOrAddMessageInRedis();
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