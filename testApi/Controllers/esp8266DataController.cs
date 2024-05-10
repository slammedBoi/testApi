using Amazon.DynamoDBv2.DataModel;
using Microsoft.AspNetCore.Mvc;
using testApi.Models;

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
            List<esp8266Data> products = await _context.ScanAsync<esp8266Data>(default).GetRemainingAsync();
            
            return Ok(products);
        }
    } 
}