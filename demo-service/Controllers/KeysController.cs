using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using demo_service.Dto;
using demo_service.RabbitMQServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace demo_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class KeysController : ControllerBase
    {
        protected readonly IRabbitMQService _rabbitMQService;
        public KeysController(IRabbitMQService rabbitMQService)
        {
            _rabbitMQService = rabbitMQService;
        }

        // GET api/values
        [HttpGet]
        public ActionResult Get()
        {
            var result = _rabbitMQService.Recive<KeyOutDto>("keyState", false);
            return Ok(result);
        }

        // GET api/id
        [HttpGet("{id}/{keyLogSlug}")]
        public ActionResult Get(string id,  string keyLogSlug)
        {
            List<KeyOutDto> result = _rabbitMQService.Recive<KeyOutDto>(keyLogSlug, false);

            var res = result.Where(s=> s.Name == id).ToList();

            return Ok(res);
        }
    }
}
