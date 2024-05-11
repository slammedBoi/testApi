using Newtonsoft.Json;
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

        public List<esp8266Data> getData()
        {
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "api/esp8266Data/GetAll").Result;

            if (response.IsSuccessStatusCode == true)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                temperatureData = JsonConvert.DeserializeObject<List<esp8266Data>>(data);
            }

            return temperatureData;
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
                    //danielsData.Add(data);
                }
            }

            //temp sinced daniel has no data
            DateTime currentDateTime = DateTime.Now;

            esp8266Data temp = new esp8266Data();
            temp.timestamp = currentDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            temp.humidity = "0";
            temp.temperature = "0";
            temp.thing = "esp8266-Daniel";

            danielsData.Add(temp);

            filteredData.Add(briansData);
            filteredData.Add(seansData);    
            filteredData.Add(danielsData);

            return filteredData;
        }
    }
}
