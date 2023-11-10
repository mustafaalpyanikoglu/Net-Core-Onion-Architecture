using Core.Utilities.Abstract;
using Core.Utilities.Concrete;

namespace Core.Application.Algorithms;

public interface ISimulatedAnnealing
{
    IDataResult<BestResult> SolveWarehouseLocationProblem();
}
