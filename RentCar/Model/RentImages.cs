using System.Collections.Generic;

namespace RentCar.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class RentImages
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, ForeignKey("BookingModel")]
        public int BookingId { get; set; } // Foreign Key to BookingModel
        public BookingModel Booking { get; set; } // Navigation property

        [Required, ForeignKey("Car")]
        public int CarId { get; set; } // Foreign Key to CarModel
        public CarModel Car { get; set; } // Navigation property

        [Required, ForeignKey("Customer")]
        public int CustomerId { get; set; } // Foreign Key to CustomerModel
        public CustomerModel Customer { get; set; } // Navigation property

        public bool IsBeforeRental { get; set; } // Indicates if the image was taken before rental

        public DateTime UploadDate { get; set; } // Date and time when the image was uploaded

        public string ImageUrl1 { get; set; } // URL or path to the image
        public string ImageUrl2 { get; set; } // URL or path to the image
        public string ImageUrl3 { get; set; } // URL or path to the image
        public string ImageUrl4 { get; set; } // URL or path to the image
    }
}