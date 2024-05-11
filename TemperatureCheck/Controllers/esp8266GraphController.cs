using Microsoft.AspNetCore.Mvc;

namespace TemperatureCheck.Controllers
{
    public class esp8266GraphController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
