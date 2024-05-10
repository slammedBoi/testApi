using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Globalization;
using testApi.Models;

namespace TemperatureCheck.Controllers
{
    public class esp8266DataController : Controller
    {
        Uri baseAddr = new Uri("https://localhost:7032");
        private HttpClient _client;

        public esp8266DataController()
        { 
            _client = new HttpClient();
            _client.BaseAddress = baseAddr;
        }

        public IActionResult Index()
        {
            List<esp8266Data> tempList = new List<esp8266Data>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "api/esp8266Data/GetAll").Result;

            if(response.IsSuccessStatusCode == true)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                tempList = JsonConvert.DeserializeObject<List<esp8266Data>>(data);
            }

            return View(tempList);
        }
    }
}
