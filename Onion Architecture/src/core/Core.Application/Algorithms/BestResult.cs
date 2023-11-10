namespace Core.Application.Algorithms;

public class BestResult
{
    public double Cost { get; set; }
    public List<int> Assignments { get; set; }

    public BestResult(double cost, List<int> assignments)
    {
        Cost = cost;
        Assignments = assignments;
    }
}
