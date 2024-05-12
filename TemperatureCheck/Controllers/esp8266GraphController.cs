using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TempCommon;
using TemperatureCheck.Models;

namespace TemperatureCheck.Controllers
{
    public class esp8266GraphController : Controller
    {
        DynamoBusiness.DynamoClient dynamoReference { get; set; }

        public esp8266GraphController() 
        {
            dynamoReference = new DynamoBusiness.DynamoClient();
        }

        public IActionResult Index()
        {
            List<List<esp8266Data>> filteringCompleted = new List<List<esp8266Data>>();
            List<esp8266Data> tempList = new List<esp8266Data>();

            tempList = dynamoReference.getData();

            //filtering must be done
            filteringCompleted = dynamoReference.filterData(tempList);

            List<DataPoint> brianTemp = new List<DataPoint>();
            List<DataPoint> brianHumid = new List<DataPoint>();

            List<DataPoint> seanTemp = new List<DataPoint>();
            List<DataPoint> seanHumid = new List<DataPoint>();

            List<DataPoint> danielTemp = new List<DataPoint>();
            List<DataPoint> danielHumid = new List<DataPoint>();

            for(int i = 0; i < 3; i++)
            {
                foreach(var data in filteringCompleted[i])
                {
                    if(i == 0)
                    {
                        brianTemp.Add(new DataPoint(data.timestamp, Convert.ToDouble(data.temperature)));
                        brianHumid.Add(new DataPoint(data.timestamp, Convert.ToDouble(data.humidity)));
                    }
                    else if(i == 1)
                    {
                        seanTemp.Add(new DataPoint(data.timestamp, Convert.ToDouble(data.temperature)));
                        seanHumid.Add(new DataPoint(data.timestamp, Convert.ToDouble(data.humidity)));
                    }
                    else
                    {
                        danielTemp.Add(new DataPoint(data.timestamp, Convert.ToDouble(data.temperature)));
                        danielHumid.Add(new DataPoint(data.timestamp, Convert.ToDouble(data.humidity)));
                    }
                }
            }

            ViewBag.BrianTemp = JsonConvert.SerializeObject(brianTemp);
            ViewBag.BrianHumid = JsonConvert.SerializeObject(brianHumid);

            ViewBag.SeanTemp = JsonConvert.SerializeObject(seanTemp);
            ViewBag.SeanHumid = JsonConvert.SerializeObject(seanHumid);

            ViewBag.DanielTemp = JsonConvert.SerializeObject(danielTemp);
            ViewBag.DanielHumid = JsonConvert.SerializeObject(danielHumid);

            return View(filteringCompleted);
        }
    }
}
