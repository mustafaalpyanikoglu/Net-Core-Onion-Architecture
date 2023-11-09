﻿using Core.Persistence.Repositories;

namespace Domain.Concrete;

public class Warehouse:Entity
{
    public int Capacity { get; set; }
    public double SetupCost { get; set; }

    public Warehouse()
    {
        
    }

    public Warehouse(int id, int capacity, double setupCost)
    {
        Id = id;
        Capacity = capacity;
        SetupCost = setupCost;
    }
}
