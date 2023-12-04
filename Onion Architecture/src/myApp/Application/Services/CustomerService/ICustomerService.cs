using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Concrete;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.CustomerService;

public interface ICustomerService
{
    Task<List<Customer>> GetListCustomer();
    Task<List<Customer>> GetListCustomerWarehouseCosts();
}

public class CustomerManager : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerManager(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<List<Customer>> GetListCustomer()
    {
        IPaginate<Customer> customers =  await _customerRepository.GetListAsync();
        return customers.Items.ToList();
    }

    public async Task<List<Customer>> GetListCustomerWarehouseCosts()
    {
        IPaginate<Customer> customers = await _customerRepository.GetListAsync(include: t => t.Include(t => t.CustomerWarehouseCosts));
        return customers.Items.ToList();
    }
}