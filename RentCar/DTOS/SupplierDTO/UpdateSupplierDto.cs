using System.ComponentModel.DataAnnotations;

namespace RentCar.DTOS.SupplierDTO;

public class UpdateSupplierDto
{
    [Required]
    public int Id { get; set; }

    public string? Name { get; set; }
    public string? ContactNumber { get; set; }
    public string? Email { get; set; }
}
