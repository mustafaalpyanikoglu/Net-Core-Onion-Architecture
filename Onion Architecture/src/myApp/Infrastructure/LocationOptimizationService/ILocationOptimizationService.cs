using Application.Features.LocationSolvers.Dtos;

namespace Infrastructure.LocationOptimizationService
{
    public interface ILocationOptimizationService
    {
        Task<LocationSolverResult> LocationSolver();
    }
}
