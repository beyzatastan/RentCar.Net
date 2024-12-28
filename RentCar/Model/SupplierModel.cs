using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentCar.Model;

public class SupplierModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Id otomatik olarak oluşturulacak
        public int Id { get; set; } // Primary Key
        public string CompanyName { get; set; } // Supplier company name
        public string ContactPerson { get; set; } // Contact person
        public string ContactNumber { get; set; } // Phone
        public string Address { get; set; } // Phone
        public string TaxNumber { get; set; }
        public string Email { get; set; } // Email
        public  string LogoUrl { get; set; }
        public Double Rating { get; set; }
        public ICollection<CarModel> Cars { get; set; } // List of cars provided by this supplier
        public ICollection<ReviewModel> Reviews { get; set; } // Tedarikçinin incelemeleri


}