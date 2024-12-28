using System;
using System.ComponentModel.DataAnnotations;

namespace RentCar.DTOS.SupplierDTO;

public class AddSupplierDto
{
    [Required]
    public string CompanyName  { get; set; }
    [Required]
    public string Address { get; set; }
    [Required]
    public string ContactPerson { get; set; }
    public string ContactNumber { get; set; }
    public string TaxNumber { get; set; }
    public string Email { get; set; }
    public  string LogoUrl { get; set; }

}