using Newtonsoft.Json;
using System.Collections.Generic;
using System.Globalization;
using TempCommon;

namespace DynamoBusiness
{
    public class DynamoClient
    {
        Uri baseAddr = new Uri("https://localhost:7032");
        private HttpClient _client;

        public List<esp8266Data> temperatureData {  get; set; }

        public DynamoClient()
        {
            temperatureData = new List<esp8266Data>();

            _client = new HttpClient();
            _client.BaseAddress = baseAddr;
        }

        //retrieves data from Rest Web API method
        public List<esp8266Data> getData()
        {
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "api/esp8266Data/GetAll").Result;

            if (response.IsSuccessStatusCode == true)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                temperatureData = JsonConvert.DeserializeObject<List<esp8266Data>>(data);
            }

            //sorts data from descending order to display newest information first
            temperatureData = sortingData(temperatureData);

            return temperatureData;
        }

        public List<DateTime> sortDates(List<DateTime> dates)
        {
            //sorts dates in descending order
			dates.Sort((a, b) => b.CompareTo(a));
			return dates;
        }

        public List<esp8266Data> sortingData(List<esp8266Data> temperatureData)
        {
			Dictionary<DateTime, esp8266Data> mapping = new Dictionary<DateTime, esp8266Data>();
			List<DateTime> dates = new List<DateTime>();
			List<esp8266Data> sortedDData = new List<esp8266Data>();

			foreach (var data in temperatureData)
			{
				int index = data.timestamp.IndexOf("T") + 1;
				int indexZ = data.timestamp.IndexOf("Z");

				string time = data.timestamp.Substring(index, 8);
				DateTime holder = DateTime.ParseExact(time, "HH:mm:ss", CultureInfo.InvariantCulture);

				mapping.Add(holder, data);
				dates.Add(holder);
			}

			dates = sortDates(dates);
			foreach (var value in dates)
			{
				sortedDData.Add(mapping[value]);
			}

            return sortedDData;
		}

        public List<List<esp8266Data>> filterData(List<esp8266Data> totalData)
        {
            List<List<esp8266Data>> filteredData = new List<List<esp8266Data>>();

            List<esp8266Data> briansData = new List<esp8266Data>();
            List<esp8266Data> seansData = new List<esp8266Data>();
            List<esp8266Data> danielsData = new List<esp8266Data>();

            foreach (var data in totalData)
            {
                int index = data.thing.IndexOf("-") + 1;
                if(data.thing.Substring(index) == "Brian")
                {
                    briansData.Add(data);
                }
                else if (data.thing.Substring(index) == "Sean")
                {
                    seansData.Add(data);
                }
                else if (data.thing.Substring(index) == "Daniel")
                {
                    //re-add when daniel runs arduino
                    danielsData.Add(data);
                }
            }

            //temp sinced daniel has no data
            //DateTime currentDateTime = DateTime.Now;

            //esp8266Data temp = new esp8266Data();
            //temp.timestamp = currentDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            //temp.humidity = "0";
            //temp.temperature = "0";
            //temp.thing = "esp8266-Daniel";

            //danielsData.Add(temp);

            filteredData.Add(briansData);
            filteredData.Add(seansData);    
            filteredData.Add(danielsData);

            return filteredData;
        }
    }
}
