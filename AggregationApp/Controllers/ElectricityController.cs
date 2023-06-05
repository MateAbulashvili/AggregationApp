using AggregationApp.Data;
using AggregationApp.Models;
using AggregationApp.Services;
using Microsoft.AspNetCore.Mvc;
namespace AggregationApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElectricityController : ControllerBase
    {
        private readonly ILogger<ElectricityController> _logger;
        private readonly IElectricityDataService _electricityDataService;
        private readonly ElectricityDbContext _context;

        public ElectricityController(ILogger<ElectricityController> logger, IElectricityDataService electricityDataService, ElectricityDbContext context)
        {
            _logger = logger;
            _electricityDataService = electricityDataService;
            _context = context;

        }

        [HttpGet("GetData")]
        public async Task<ActionResult<IEnumerable<APIResponseModel>>> GetAggregatedData()
        {
            _logger.LogInformation("GET request received for aggregated data.");

            try
            {
                var aggregatedData = await _electricityDataService.GetAggregatedDataAsync();
                if (aggregatedData != null)
                {
                    return Ok(aggregatedData);
                }
                else
                    return BadRequest();
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve aggregated data.");
                return StatusCode(500, "An error occurred while retrieving the data.");
            }
        }
        [HttpGet("download")]
        public async Task<IActionResult> Download()
        {
            _logger.LogInformation("GET request received for aggregated data.");

            try
            {
                var result = await _electricityDataService.DownloadCsvFiles();
                if (result)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve aggregated data.");
                return StatusCode(500, "An error occured while downloading data");
            }
        }
    }
}
