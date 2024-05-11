using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempCommon
{
    [DynamoDBTable("esp8266Data")]
    public class esp8266Data
    {
        [DynamoDBProperty("timestamp")]
        public string timestamp { get; set; }
        [DynamoDBProperty("humidity")]
        public string humidity { get; set; }
        [DynamoDBProperty("temperature")]
        public string temperature { get; set; }
        [DynamoDBProperty("thing")]
        public string thing { get; set; }

    }
}
