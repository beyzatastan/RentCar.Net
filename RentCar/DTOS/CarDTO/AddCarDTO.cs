using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace RentCar.CarDTO;
public class AddCarDto
{
    [Required]
    public string Brand { get; set; }

    [Required]
    public string Model { get; set; }

    [Range(1900, 2100, ErrorMessage = "Year must be between 1900 and 2100")]
    public int Year { get; set; }

    [Required]
    public string LicensePlate { get; set; }

    public string TransmissionType { get; set; }
    public int SeatCount { get; set; }
    public decimal DailyPrice { get; set; }
    public int SupplierId { get; set; }
    public decimal Deposit { get; set; } // Daily rental price
    public string CarClass { get; set; } // Transmission type
    public string GasType { get; set; } // Transmission type
    public int LocationId { get; set; }
    
    // Resim dosyaları (isteğe bağlı)
    public List<IFormFile>? Images { get; set; }
}
public enum TransmissionType
{
    Manual,
    Automatic
}