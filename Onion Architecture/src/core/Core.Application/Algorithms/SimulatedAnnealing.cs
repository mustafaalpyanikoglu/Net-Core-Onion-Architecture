using Core.Utilities.Abstract;
using Core.Utilities.Concrete;
using Domain.Concrete;
using static Core.Application.Algorithms.SimulatedAnnealingConstants;

namespace Core.Application.Algorithms;

public class SimulatedAnnealing: ISimulatedAnnealing
{
    private Random _random = new Random(); // Random değer atamak için
    private int _numWarehouses { get; set; }
    private int _numCustomers { get; set; }
    private List<Customer> _customers;  // Müşteri listesi
    private List<Warehouse> _warehouses;  // Depo listesi
    private Dictionary<int, int> _solution; // _solution veri tipi Dictionary<int, int> olarak değiştirildi

    private readonly IQuickSort _quickSort;

    public Dictionary<int, int> Solution // Solution property'sinin veri tipi değiştirildi
    {
        get { return _solution; }
        private set { _solution = value; }
    }

    public SimulatedAnnealing(IQuickSort quickSort)
    {
        _quickSort = quickSort;
    }

    public IDataResult<BestResult> SolveWarehouseLocationProblem(List<Customer> customers, List<Warehouse> warehouses)
    {
        _customers = customers;
        _warehouses = warehouses;
        _numWarehouses = warehouses.Count;
        _numCustomers = customers.Count;

        Dictionary<int, int> currentSolution = GenerateRandomSolution(); // currentSolution veri tipi değiştirildi
        double currentCost = CalculateCost(currentSolution.Values.ToList());

        Dictionary<int, int> bestSolution = new Dictionary<int, int>(currentSolution); // bestSolution veri tipi değiştirildi
        double bestCost = currentCost;

        double temperature = INITAL_TEMPERATURE;
        int iteration = 0;

        while (temperature > 0 && iteration < MAX_ITERATIONS)
        {
            Dictionary<int, int> newSolution = GenerateNeighborSolution(currentSolution); // newSolution veri tipi değiştirildi
            double newCost = CalculateCost(newSolution.Values.ToList());

            if (shouldAcceptNewSolution(currentCost, newCost))
            {
                currentSolution = new Dictionary<int, int>(newSolution);
                currentCost = newCost;
            }
            if (isNewCostBetter(newCost, bestCost))
            {
                bestSolution = new Dictionary<int, int>(newSolution);
                bestCost = newCost;
            }
            temperature *= COOLINGRATE;
            iteration++;
        }

        return new SuccessDataResult<BestResult>(new BestResult(bestCost, bestSolution.Values.ToList()));
    }
    public double CalculateCost(List<int> solution)
    {
        double totalCost = 0;
        for (int i = 0; i < _numCustomers; i++)
        {
            totalCost += CalculateCostForSelectedWarehouse(solution[i], i);
        }

        return totalCost;
    }
    private Dictionary<int, int> GenerateRandomSolution()
    {
        List<Customer> customers = new List<Customer>(_customers);
        List<Warehouse> warehouses = new List<Warehouse>(_warehouses);
        Dictionary<int, int> solution = new Dictionary<int, int>();

        _quickSort.SortBy(customers, 0, customers.Count - 1, _quickSort.CompareCustomersByDemand);
        _quickSort.SortBy(warehouses, 0, warehouses.Count - 1, _quickSort.CompareWarehousesBySetupCost);

        foreach (var customer in customers)
        {
            Warehouse selectedWarehouse = findSuitableWarehouse(customer, warehouses);
            if (selectedWarehouse is not null)
            {
                selectedWarehouse.Capacity -= customer.Demand;
                solution[customer.Id] = selectedWarehouse.Id;
            }
            else
            {
                // Uygun bir depo bulunamadı
            }
        }

        return solution;
    }

    private Dictionary<int, int> GenerateNeighborSolution(Dictionary<int, int> currentSolution)
    {
        Dictionary<int, int> newSolution = new Dictionary<int, int>(currentSolution);
        int randomCustomerId = _customers[_random.Next(_numCustomers)].Id;

        // Rastgele bir depo ID'si seç
        int randomWarehouseId = _warehouses[_random.Next(_numWarehouses)].Id;

        newSolution[randomCustomerId] = randomWarehouseId;

        return newSolution;
    }
    private double CalculateCostForSelectedWarehouse(int warehouseId, int customerId)
    {
        CustomerWarehouseCost? travel = _customers[customerId].CustomerWarehouseCosts.Where(p => p.WarehouseID == warehouseId).FirstOrDefault();
        Warehouse? setup = _warehouses.Where(p => p.Id == warehouseId).FirstOrDefault();

        if(travel is not null && setup is not null)
        {
            double travelCost = travel.Cost;
            double setupCost = setup.SetupCost;
            return travelCost + setupCost;
        }
        else return 0;
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
