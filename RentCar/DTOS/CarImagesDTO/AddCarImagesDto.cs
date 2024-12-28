using System;

namespace RentCar.DTOS.CarImagesDTO;

public class AddCarImagesDto
{
    public int BookingId { get; set; } // Foreign Key to BookingModel
    
    public int CarId { get; set; } // Foreign Key to CarModel
    

    public int CustomerId { get; set; } // Foreign Key to CustomerModel

    public bool IsBeforeRental { get; set; } // Indicates if the image was taken before rental

    public DateTime UploadDate { get; set; } // Date and time when the image was uploaded

    public string ImageUrl1 { get; set; } // URL or path to the image
    public string ImageUrl2 { get; set; } // URL or path to the image
    public string ImageUrl3 { get; set; } // URL or path to the image
    public string ImageUrl4 { get; set; } // URL or path to the image

}