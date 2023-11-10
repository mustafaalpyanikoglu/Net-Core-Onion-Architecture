using Core.Utilities.Abstract;
using Core.Utilities.Concrete;
using Domain.Concrete;
using static Core.Application.Algorithms.SimulatedAnnealingConstants;

namespace Core.Application.Algorithms;

public class SimulatedAnnealing: ISimulatedAnnealing
{
    private Random _random = new Random(); // Random değer atamak için
    private int _numWarehouses { get; set; }
    private int __numCustomers { get; set; }
    private List<Customer> _customers;  // Müşteri listesi
    private List<Warehouse> _warehouses;  // Depo listesi
    private List<CustomerWarehouseCost> _customerWarehouseCosts;
    private readonly IQuickSort _quickSort;
    private List<int> _solution;
    public List<int> Solution
    {
        get { return _solution; }
        private set { _solution = value; }
    }

    public SimulatedAnnealing(IQuickSort quickSort, List<Customer> customers, List<Warehouse> warehouses, List<CustomerWarehouseCost> customerWarehouseCosts)
    {
        _customers = customers;
        _warehouses = warehouses;
        _customerWarehouseCosts = customerWarehouseCosts;
        _numWarehouses = warehouses.Count;
        __numCustomers = customers.Count;
        _quickSort = quickSort;
    }

    public IDataResult<BestResult> SolveWarehouseLocationProblem()
    {
        List<int> currentSolution = GenerateRandomSolution();
        double currentCost = CalculateCost(currentSolution);

        List<int> bestSolution = new List<int>(currentSolution);
        double bestCost = currentCost;

        double temperature = INITAL_TEMPERATURE;
        int iteration = 0;

        while (temperature > 0 && iteration < MAX_ITERATIONS)
        {
            List<int> newSolution = GenerateNeighborSolution(currentSolution);
            double newCost = CalculateCost(newSolution);

            if (shouldAcceptNewSolution(currentCost, newCost))
            {
                currentSolution = new List<int>(newSolution);
                currentCost = newCost;
            }
            if (isNewCostBetter(newCost,bestCost))
            {
                bestSolution = new List<int>(newSolution);
                bestCost = newCost;
            }
            temperature *= COOLINGRATE;
            iteration++;
        }

        return new SuccessDataResult<BestResult>(new BestResult(bestCost,bestSolution));
    }
    public double CalculateCost(List<int> solution)
    {
        double totalCost = 0;
        for (int i = 0; i < __numCustomers; i++)
        {
            totalCost += CalculateCostForSelectedWarehouse(solution[i], i);
        }

        return totalCost;
    }
   
    private List<int> GenerateRandomSolution()
    {
        List<Customer> customers = new List<Customer>(_customers);
        List<Warehouse> warehouses = new List<Warehouse>(_warehouses);
        int[] solution = new int[customers.Count];

        _quickSort.SortBy(customers, 0, customers.Count - 1, _quickSort.CompareCustomersByDemand);
        _quickSort.SortBy(warehouses, 0, warehouses.Count - 1, _quickSort.CompareWarehousesBySetupCost);


        for (int i = 0; i < __numCustomers; i++)
        {
            Warehouse selectedWarehouse = findSuitableWarehouse(customers[i], warehouses);
            if (selectedWarehouse is not null)
            {
                selectedWarehouse.Capacity -= customers[i].Demand;
                solution[customers[i].Id] = selectedWarehouse.Id;
            }
            else
            {
                // Uygun bir depo bulunamadı
            }
        }

        return solution.ToList();
    }
    private List<int> GenerateNeighborSolution(List<int> currentSolution)
    {
        List<int> newSolution = new List<int>(currentSolution);
        int customerIndex = _random.Next(__numCustomers);
        int randomWarehouse = _random.Next(_numWarehouses);

        newSolution[customerIndex] = randomWarehouse;

        return newSolution;
    }
    private double CalculateCostForSelectedWarehouse(int warehouseId, int customerId)
    {
        double travelCost = _customerWarehouseCosts.Where(p => p.WarehouseID == warehouseId && p.CustomerId == customerId).First().Cost;
        double setupCost = _warehouses.Where(p => p.Id == warehouseId).First().SetupCost;
        return travelCost + setupCost;
    }
    private bool shouldAcceptNewSolution(double currentCost, double newCost)
    {
        if (isNewCostBetterThanCurrent(newCost, currentCost)) return true;

        double acceptanceProbability = calculateAcceptanceProbability(currentCost, newCost);
        double randomValue = _random.NextDouble();

        return randomValue < acceptanceProbability;
    }
    private Warehouse findSuitableWarehouse(Customer customer, List<Warehouse> warehouses) => warehouses.FirstOrDefault(item => isCapacitySufficient(item.Capacity, customer.Demand));
    private bool isNewCostBetter(double newCost, double bestCost) => newCost < bestCost;
    private bool isNewCostBetterThanCurrent(double newCost, double currentCost) => newCost < currentCost;
    private bool isCapacitySufficient(int capacity, int demand) => capacity >= demand;
    private double calculateAcceptanceProbability(double currentCost, double newCost) => Math.Exp((currentCost - newCost) / INITAL_TEMPERATURE);

}
