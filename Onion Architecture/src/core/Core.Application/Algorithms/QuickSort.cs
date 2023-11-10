using Domain.Concrete;

namespace Core.Application.Algorithms;

public class QuickSort:IQuickSort
{
    public void SortBy<T>(List<T> list, int low, int high, Comparison<T> comparison)
    {
        if (low < high)
        {
            int partitionIndex = Partition(list, low, high, comparison);
            SortBy(list, low, partitionIndex - 1, comparison);
            SortBy(list, partitionIndex + 1, high, comparison);
        }
    }
    public int CompareCustomersByDemand(Customer c1, Customer c2)
    {
        return c1.Demand.CompareTo(c2.Demand);
    }

    public int CompareWarehousesBySetupCost(Warehouse w1, Warehouse w2)
    {
        return w1.SetupCost.CompareTo(w2.SetupCost);
    }
    private int Partition<T>(List<T> list, int low, int high, Comparison<T> comparison)
    {
        T pivot = list[high];
        int i = low - 1;

        for (int j = low; j < high; j++)
        {
            if (comparison.Invoke(list[j], pivot) <= 0)
            {
                i++;
                Swap(list, i, j);
            }
        }

        Swap(list, i + 1, high);
        return i + 1;
    }

    private void Swap<T>(List<T> list, int i, int j)
    {
        T temp = list[i];
        list[i] = list[j];
        list[j] = temp;
    }
}
