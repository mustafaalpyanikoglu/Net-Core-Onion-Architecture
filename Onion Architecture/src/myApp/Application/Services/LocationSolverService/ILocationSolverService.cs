using Core.Application.Algorithms;

namespace Application.Services.LocationSolverService;

public interface ILocationSolverService
{
    Task<BestResult> SimaulatedAnnealingQuickSortSolver();
}
