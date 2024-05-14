using Microsoft.AspNetCore.Mvc;
using TempCommon;
using DynamoBusiness;

namespace TemperatureCheck.Controllers
{
    public class esp8266DataController : Controller
    {
        //references the DynamoDB client to acceess the data from DynamoDB
        DynamoBusiness.DynamoClient dynamoReference {  get; set; }

        public esp8266DataController()
        { 
            dynamoReference = new DynamoClient();
        }

        public IActionResult Index()
        {
            List<List<esp8266Data>> filteringCompleted = new List<List<esp8266Data>>();
            List<esp8266Data> tempList = new List<esp8266Data>();

            //gets all data from database (unfiltered)
            tempList = dynamoReference.temperatureData;//dynamoReference.getData();

            //filtering must be done
            //filteres data into specified lists, one for each microcontroller
            filteringCompleted = dynamoReference.filterData(tempList);

            return View(filteringCompleted);
        }
    }
}
