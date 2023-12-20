using Application.Features.LocationSolvers.Dtos;
using Infrastructure.LocationOptimizationService;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LocationSolverController : BaseController
{
    private readonly ILocationOptimizationService _locationOptimizationService;

    public LocationSolverController(ILocationOptimizationService locationOptimizationService)
    {
        _locationOptimizationService = locationOptimizationService;
    }

    [ProducesResponseType(typeof(LocationSolverResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(LocationSolverResult), StatusCodes.Status400BadRequest)]
    [HttpPost("locationsolver")]
    public async Task<IActionResult> LocationSolver()
    {
        LocationSolverResult result = await _locationOptimizationService.LocationSolver();
        return Ok(result);
    }
}
