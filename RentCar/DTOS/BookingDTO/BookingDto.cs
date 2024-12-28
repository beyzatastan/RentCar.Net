using System;

namespace RentCar.DTOS.BookingDTO;

public class BookingDto
{
    public int BookingId { get; set; }
    public string CarModel { get; set; }
    public int CustomerId { get; set; }
    public int CarId { get; set; }
    public string StartLocation { get; set; }
    public string EndLocation { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}