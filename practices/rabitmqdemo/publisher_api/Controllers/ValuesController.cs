using Microsoft.AspNetCore.Mvc;
using publisher_api.Services;
using System;
using System.Collections.Generic;

namespace publisher_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ValuesController : ControllerBase
    {

        private readonly IMessageService _messageService;
        public ValuesController(IMessageService messageService)
        {
            _messageService = messageService;
        }


        //GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "mahedee", "hasan" };
        }

        [HttpPost]
        public void Post([FromBody] string payload)
        // public IActionResult Post([FromBody] string payload)
        //public IActionResult Post()
        {

            Console.WriteLine("received a Post: " + payload);
            _messageService.Enqueue(payload);
            //return Ok("{\"success\": \"true\"}");
        }
    }
}