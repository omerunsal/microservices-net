using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers
{
    [Route("api/c/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        public PlatformsController()
        {

        }

        public ActionResult TestInboundConnection()
        {
            System.Console.WriteLine("TEST POST from Command Service");
            return Ok("Inbound test of from Platforms Controller");
            
        }
    }
}