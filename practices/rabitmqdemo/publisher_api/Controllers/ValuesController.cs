using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace publisher_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ValuesController : ControllerBase
    {
        //GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "mahedee", "hasan" };
        }

        [HttpPost]
        public IActionResult Post([FromBody] string payload)
        //public IActionResult Post()
        {
            return Ok("{\"success\": \"true\"}");
        }
    }
}