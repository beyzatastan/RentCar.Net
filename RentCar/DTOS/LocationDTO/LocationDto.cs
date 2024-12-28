using System.ComponentModel.DataAnnotations;

namespace RentCar.DTOS;

public class LocationDto
{
    [Required]
    public string City { get; set; } // City name
   
}