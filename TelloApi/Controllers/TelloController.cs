using Microsoft.AspNetCore.Mvc;
using TelloApi.Services;

namespace TelloApi.Controllers
{
    [Route("api/[controller]")]
    public class TelloController : Controller
    {
        [HttpGet]
        public string Get()
        {
            return "Welcome to the tello api";
        }
   
        [HttpGet("query/{query}")]
        public string Get(string query)
        {
            var svc = new TelloSdkService();
            return svc.QueryBattery();
        }
        
        [HttpGet("flip/{direction}")]
        public string Flip(string direction)
        {
            var svc = new TelloSdkService();
            return svc.Flip(direction);
        }

        [HttpGet("move/{direction}/{distance}")]
        public string Move(string direction,int distance)
        {
            var svc = new TelloSdkService();
            //TODO: validate distance
            return svc.Move(direction, distance);
        }

        [HttpGet("rotate/{direction}/")]
        public string Rotate(string direction, int degrees)
        {
            var svc = new TelloSdkService();
            //TODO: validate degrees
            return svc.Roate(direction, degrees);
        }

        [HttpGet("takeoff")]
        public void Takoff()
        {
            var svc = new TelloSdkService();
            svc.TakeOff();
        }

        [HttpGet("land")]
        public void Land()
        {
            var svc = new TelloSdkService();
            svc.Land();
        }

    
    }
}
