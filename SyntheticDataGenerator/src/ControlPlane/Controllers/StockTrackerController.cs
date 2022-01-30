using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace ControlPlane.Controllers;

[ApiController]
[Route("[controller]")]
public class StockTrackerController : Controller
{

    public StockTrackerController(ILogger<StockTrackerController> logger,
     IStockManagerService tickerService)
    {
        _logger = logger;
        _tickerService = tickerService;
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] TickerDto tickerDto)
    {
        try
        {
            var addedStock = await _tickerService.AddStock(tickerDto.ticker);
            if (addedStock == null)
                return NotFound();

            return Ok(addedStock);
        }
        catch
        {
            return StatusCode(500);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var result = await _tickerService.GetAllStocks();
            return Ok(result.Select(x => new StockDto()
            {
                ticker = x.ticker,
                tickerName = x.tickerName
            }).ToList());
        }
        catch
        {
            return StatusCode(500);
        }
    }

    [HttpGet("{ticker}")]
    public async Task<IActionResult> Get(string ticker)
    {
        try
        {
            var result = await _tickerService.GetStock(ticker);
            if (result == null)
                return NotFound();

            return Ok(
                new StockDto
                {
                    ticker = result.ticker,
                    tickerName = result.tickerName
                }
            );
        }
        catch
        {
            return StatusCode(500);
        }
    }
    [HttpDelete(Name = "DeleteStock")]
    public async Task<IActionResult> DeleteAsync([FromQuery] string ticker)
    {
        try
        {
            var result = await _tickerService.DeleteStock(ticker);
            if (!result)
                return NotFound();

            return Ok();
        }
        catch
        {
            return StatusCode(500);
        }
    }

    private readonly ILogger<StockTrackerController> _logger;
    private readonly IStockManagerService _tickerService;

}
