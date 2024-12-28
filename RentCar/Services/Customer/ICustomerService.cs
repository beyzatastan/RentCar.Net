using System.Collections.Generic;
using System.Threading.Tasks;
using RentCar.Model;

namespace RentCar.Services.Customer;

public interface ICustomerService
{
    Task<List<CustomerModel>>? GetAllCustomers();
    Task<CustomerModel>? GetSingleCustomer(int id);
    
    Task<List<CustomerModel>>? AddCustomer(CustomerModel customer);
    
    Task<List<CustomerModel>>? UpdateCustomer(int id, CustomerModel request);
    Task<List<CustomerModel>>? DeleteCustomer(int id);
}
