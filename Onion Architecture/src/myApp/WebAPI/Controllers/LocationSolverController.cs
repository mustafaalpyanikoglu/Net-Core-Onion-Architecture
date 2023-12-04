using Application.Services.LocationSolverService;
using Core.Application.Algorithms;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LocationSolverController : BaseController
{
    private readonly ILocationSolverService _locationSolverService;

    public LocationSolverController(ILocationSolverService locationSolverService)
    {
        _locationSolverService = locationSolverService;
    }

    [ProducesResponseType(typeof(BestResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    [HttpPost("locationsolver")]
    public async Task<IActionResult> LocationSolver()
    {
        BestResult result = await _locationSolverService.SimaulatedAnnealingQuickSortSolver();
        return Ok(result);
    }
}
