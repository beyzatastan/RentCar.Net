using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentCar.DTOS.SupplierDTO;
using RentCar.Model;
using RentCar.Data;

namespace RentCar.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SupplierController : ControllerBase
{
    private readonly DataContext _context;

    public SupplierController(DataContext context)
    {
        _context = context;
    }

    // POST: api/Supplier
    [HttpPost("addSupplier")]
    public IActionResult AddSupplier([FromBody] AddSupplierDto supplierDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var supplier = new SupplierModel
        {
            CompanyName = supplierDto.CompanyName,
            ContactPerson = supplierDto.ContactPerson,
            Address = supplierDto.Address,
            ContactNumber = supplierDto.ContactNumber,
            Email = supplierDto.Email,
            LogoUrl = supplierDto.LogoUrl,
            TaxNumber = supplierDto.TaxNumber,
            
        };

        _context.Suppliers.Add(supplier);
        _context.SaveChanges();

        return CreatedAtAction(nameof(GetSupplier), new { supplierId = supplier.Id }, supplier);
    }

    // GET: api/Supplier/{id}
    [HttpGet("getSupplierById/{supplierId}")]
    public IActionResult GetSupplier(int supplierId)
    {
        var supplier = _context.Suppliers
            .Include(s => s.Cars)       // Cars ilişkisini dahil ediyoruz
            .Include(s => s.Reviews)    // Reviews ilişkisini dahil ediyoruz
            .FirstOrDefault(s => s.Id == supplierId);

        if (supplier == null)
        {
            return NotFound();
        }

        // DTO'ya dönüştürme
        var supplierDto = new
        {
            supplier.Id,
            supplier.CompanyName,
            supplier.ContactPerson,
            supplier.ContactNumber,
            supplier.Address,
            supplier.Email,
            supplier.LogoUrl,
            supplier.Rating,
            supplier.TaxNumber,
            Cars = supplier.Cars?.Select(c => new 
            {
                c.Id,
                c.Model,
                c.Brand
            }).ToList(),
            Reviews = supplier.Reviews?.Select(r => new 
            {
                r.Id,
                r.Rating,
                r.Comment
            }).ToList()
        };

        return Ok(supplierDto);
    }
}
