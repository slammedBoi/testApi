using Amazon.DynamoDBv2.DataModel;
using Microsoft.AspNetCore.Mvc;
using TempCommon;

namespace DynamoDB.Demo.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class esp8266DataController : ControllerBase
    {
        private readonly IDynamoDBContext _context;

        public esp8266DataController(IDynamoDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //retrieves data from DynamoDb table
            //takes 5-15 seconds for data to be retrieved as I believe AWS must verify credentials of computer to ensure the table is being accessed safely
            List<esp8266Data> products = await _context.ScanAsync<esp8266Data>(default).GetRemainingAsync();
            
            return Ok(products);
        }
    } 
}