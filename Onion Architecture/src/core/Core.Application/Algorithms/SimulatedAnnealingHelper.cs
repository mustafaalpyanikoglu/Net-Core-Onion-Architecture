using Domain.Concrete;

namespace Core.Application.Algorithms;

public class SimulatedAnnealingHelper
{
    public bool CheckCapacityConstraints(List<int> solution ,List<Warehouse> warehouses, List<Customer> customers)
    {
        int warehousesCount = warehouses.Count;
        int[] warehouseCapacities = new int[warehousesCount];
        foreach (var customer in customers)
        {
            int warehouseIndex = solution[customer.Id];
            warehouseCapacities[warehouseIndex] += customer.Demand;
        }
        for (int i = 0; i < warehouses.Count; i++)
        {
            if (warehouseCapacities[i] > warehouses[i].Capacity)
            {
                return false;
            }
        }
        for (int i = 0; i < warehousesCount; i++)
        {
            if (warehouseCapacities[i] > warehouses[i].Capacity)
                Console.WriteLine($"Kapasite={warehouses[i].Capacity} Dolan Kapasite= {warehouseCapacities[i]}");
        }
        return true;
    }
    
}
