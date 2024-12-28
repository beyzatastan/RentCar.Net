using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentCar.Model;

public class ReviewModel
{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Id otomatik olarak oluşturulacak
        public int Id { get; set; } // Unique identifier for each review
        public int CustomerId { get; set; } // Foreign key linking to the user who wrote the review
        public CustomerModel Customer { get; set; } // User who wrote the review
        public int SupplierId { get; set; } // Foreign key linking to the supplier being reviewed
        public SupplierModel Supplier { get; set; } // Supplier being reviewed
        public int CarId { get; set; } // Foreign key linking to the reviewed car
        public CarModel Car { get; set; } // Reviewed car
        public int Rating { get; set; } // Rating score from 1 to 5
        public string Comment { get; set; } // Review comment
        public DateTime DateCreated { get; set; } // Date the review was created
    

}