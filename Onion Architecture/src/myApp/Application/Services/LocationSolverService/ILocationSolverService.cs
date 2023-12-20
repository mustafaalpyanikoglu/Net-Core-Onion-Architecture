using Application.Features.LocationSolvers.Dtos;

namespace Application.Services.LocationSolverService;

public interface ILocationSolverService
{
    public Task<LocationOptimizationRequestDto> SimaulatedAnnealingQuickSortSolver();
}
