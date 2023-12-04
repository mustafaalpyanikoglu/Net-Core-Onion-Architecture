using Core.Utilities.Abstract;
using Domain.Concrete;

namespace Core.Application.Algorithms;

public interface ISimulatedAnnealing
{
    IDataResult<BestResult> SolveWarehouseLocationProblem(List<Customer> customers, List<Warehouse> warehouses);
}
