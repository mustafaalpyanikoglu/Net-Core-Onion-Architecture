using Domain.Concrete;

namespace Core.Application.Algorithms;

public interface IQuickSort
{
    void SortBy<T>(List<T> list, int low, int high, Comparison<T> comparison);
    int CompareCustomersByDemand(Customer c1, Customer c2);
    int CompareWarehousesBySetupCost(Warehouse w1, Warehouse w2);
}
