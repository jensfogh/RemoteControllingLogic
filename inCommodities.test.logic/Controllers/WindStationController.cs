using System.IO;
using inCommodities.test.logic.Exceptions;
using inCommodities.test.logic.Models;
using Microsoft.AspNetCore.Mvc;

namespace inCommodities.test.logic.Controllers;

[ApiController]
[Route("[controller]")]
public class WindStationController : ControllerBase
{
    private static ControlStation station = new ControlStation(); // Optimally this should be done through dependency injection, in a way that ensures there is only ONE control station used in the application;

    [HttpGet("MarketPrice", Name = "GetMarketPrice")]
    public IActionResult GetCurrentMarketPrice()
    {
        return Ok(station.getCurrentMarkedPrice());
    }

    [HttpPut("MarketPrice/{marketPrice}", Name = "SetMarketPrice")]
    public IActionResult SetCurrentMarketPrice(uint marketPrice)
    {
        station.setCurrentMarketPrice(marketPrice);
        return Ok();
    }

    [HttpGet("ProductionTarget", Name = "GetProductionTarget")]
    public IActionResult GetCurrentProductionTarget()
    {
        return Ok(station.getCurrentProductionTarget());
    }

    [HttpPut("ProductionTarget/{delta}", Name = "UpdateProductionTarget")]
    public IActionResult UpdateCurrentProductionTarget(int delta)
    {
        try
        {
            var updatedProductionTarget = station.updateCurrentProductionTarget(delta);
            return Ok(updatedProductionTarget);
        }
        catch (InvalidDeltaException e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("ExpectedProduction", Name = "GetExpectedProduction")]
    public IActionResult GetExpectedProduction()
    {
        List<WindTurbineResponseModel> turbines = new List<WindTurbineResponseModel>();
        foreach (WindTurbine turbine in station.getTurbineStatusForWindStationA())
        {
            turbines.Add(WindTurbineResponseModel.WindTurbineResponseModelFromWindTurbineModel(turbine));
        }
        turbines = turbines.OrderBy(t => t.turbineName).ToList(); // Sort by name, to make output easier to read

        return Ok(turbines);
    }
}