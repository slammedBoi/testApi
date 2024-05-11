using Microsoft.AspNetCore.Mvc;
using TempCommon;

namespace TemperatureCheck.Controllers
{
    public class esp8266DataController : Controller
    {
        //Uri baseAddr = new Uri("https://localhost:7032");
        //private HttpClient _client;

        DynamoBusiness.DynamoClient dynamoReference {  get; set; }

        public esp8266DataController()
        { 
            //_client = new HttpClient();
            //_client.BaseAddress = baseAddr;

            dynamoReference = new DynamoBusiness.DynamoClient();
        }

        public IActionResult Index()
        {
            List<List<esp8266Data>> filteringCompleted = new List<List<esp8266Data>>();
            List<esp8266Data> tempList = new List<esp8266Data>();

            tempList = dynamoReference.getData();

            //filtering must be done
            filteringCompleted = dynamoReference.filterData(tempList);

            return View(filteringCompleted);
        }
    }
}
