using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentCar.Data;
using RentCar.DTOS.CustomerDTO;
using RentCar.Model;

namespace RentCar.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly DataContext _context;

        public CustomerController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Customer
        [HttpGet("getAllCustomers")]
        public async Task<IActionResult> GetCustomers()
        {
            var customers = await _context.Customers.ToListAsync();
            return Ok(customers);
        }
        
        // GET: api/Customer/{id}
        [HttpGet("getCustomerById/{customerId}")]
        public async Task<ActionResult<CustomerModel>> GetCustomer(int customerId)
        {
            var customer = await _context.Customers
                .Include(c => c.Reviews)   // Reviews ilişkisini yükle
                .Include(c => c.Bookings)  // Bookings ilişkisini yükle
                .FirstOrDefaultAsync(c => c.Id == customerId);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }
        
        // POST: api/Customer
        [HttpPost("addCustomer")]
        public async Task<IActionResult> AddCustomer([FromBody] AddCustomerDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Customer data is required.");
            }

            var customer = new CustomerModel
            {
                UserId = dto.UserId,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                BirthDate = dto.BirthDate,
                Address = dto.Address,
                PostalCode = dto.PostalCode,
                DrivingLicenseNumber = dto.DrivingLicenseNumber,
                DrivingLicenseIssuedDate = dto.DrivingLicenseIssuedDate,
                IdentityNumber = dto.IdentityNumber,
                City = dto.City ?? "Unknown",
                District = dto.District
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Customer added successfully", CustomerId = customer.Id });
        }

        // PUT: api/Customer/{id}
        [HttpPut("updateCustomerById/{customerId}")]
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] CustomerModel customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }

            var existingCustomer = await _context.Customers.FindAsync(id);
            if (existingCustomer == null)
            {
                return NotFound();
            }

            // Update customer details
            existingCustomer.IdentityNumber = customer.IdentityNumber;
            existingCustomer.DrivingLicenseIssuedDate = customer.DrivingLicenseIssuedDate;
            existingCustomer.DrivingLicenseNumber = customer.DrivingLicenseNumber;
            existingCustomer.BirthDate = customer.BirthDate;
            existingCustomer.PhoneNumber = customer.PhoneNumber;
            existingCustomer.City = customer.City;
            existingCustomer.District = customer.District;
            existingCustomer.Address = customer.Address;
            existingCustomer.PostalCode = customer.PostalCode;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Customer/{id}
        [HttpDelete("deleteCustomerById/{customerId}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
