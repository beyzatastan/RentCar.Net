using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RentCar.Data;
using RentCar.Model;

namespace RentCar.Services.Customer;

public class CustomerService : ICustomerService
{
    private readonly DataContext _context;

    public CustomerService(DataContext context)
    {
        _context = context;
    }
    public async Task<List<CustomerModel>>? GetAllCustomers()
    {
      var customers = await _context.Customers.ToListAsync();
      return customers;
    }

    public async Task<CustomerModel>? GetSingleCustomer(int id)
    {
        var customer =  await _context.Customers.FindAsync(id);
        if (customer == null)
            return null;
        return customer;
    }
    
    public async Task<List<CustomerModel>>? AddCustomer(CustomerModel customer)
    {
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();
        return await _context.Customers.ToListAsync();
    }

    public async Task<List<CustomerModel>>? UpdateCustomer(int id, CustomerModel request)
    {
        var customer =  await _context.Customers.FindAsync(id);
        if (customer is null)
            return null;
       
        customer.Address = request.Address;
        customer.City = request.City;
        customer.District = request.District;
        customer.BirthDate = request.BirthDate;
        customer.PostalCode = request.PostalCode;
        customer.IdentityNumber = request.IdentityNumber;
        customer.DrivingLicenseNumber = request.DrivingLicenseNumber;
        customer.DrivingLicenseIssuedDate = request.DrivingLicenseIssuedDate;
        
        await _context.SaveChangesAsync();
        return await _context.Customers.ToListAsync();
    }

    public  async Task<List<CustomerModel>>?  DeleteCustomer(int id)
    {
        var car =  await _context.Customers.FindAsync(id);
        if (car is null)
            return null;
           
        _context.Customers.Remove(car);
        await _context.SaveChangesAsync();
        return await _context.Customers.ToListAsync();
    }
}