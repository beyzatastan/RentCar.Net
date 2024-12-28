using System;
using System.ComponentModel.DataAnnotations;

namespace RentCar.DTOS.BookingDTO;


public class AddBookingDto
{
    [Required]
    public int CustomerId { get; set; }

    [Required]
    public int CarId { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime StartDate { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime EndDate { get; set; }

    [Required]
    public int StartLocationId { get; set; } 

    [Required]
    public int EndLocationId { get; set; } // Araç teslim edilecek lokasyon

}