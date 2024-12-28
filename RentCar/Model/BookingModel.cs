namespace RentCar.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text.Json.Serialization;

    public class BookingModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, ForeignKey("Customer")]
        public int CustomerId { get; set; }
        [JsonIgnore]
        public CustomerModel Customer { get; set; }

        [Required, ForeignKey("Car")]
        public int CarId { get; set; }
        public CarModel Car { get; set; }

        [Required]
        [CustomValidation(typeof(BookingModel), nameof(ValidateDates))]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; } // End date of the booking

        [ForeignKey("StartLocation")]
        public int? StartLocationId { get; set; }
        public LocationModel StartLocation { get; set; }

        [ForeignKey("EndLocation")]
        public int? EndLocationId { get; set; }
        public LocationModel EndLocation { get; set; }


        // Custom validation method
        public static ValidationResult ValidateDates(DateTime startDate, ValidationContext context)
        {
            var instance = (BookingModel)context.ObjectInstance;
            if (startDate >= instance.EndDate)
            {
                return new ValidationResult("Start date must be earlier than end date.");
            }
            return ValidationResult.Success;
        }
        
        public List<RentImages> Images { get; set; } = new List<RentImages>(); // List of images for this session
    }
}