using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RentCar.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class CustomerModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; } // Primary Key


    public string FirstName { get; set; } = string.Empty; // User's first name

    
    public string LastName { get; set; } = string.Empty; // User's last name

    [EmailAddress]
    public string Email { get; set; } = string.Empty; // User's email address for login and communication

   
    public string PhoneNumber { get; set; } = string.Empty; // Hashed password for security
 
    [ StringLength(11)]
    public string IdentityNumber { get; set; } // National ID
    
    public DateTime DrivingLicenseIssuedDate { get; set; } // Driving license issue date

    [ StringLength(20)]
    public string DrivingLicenseNumber { get; set; } // Driving license number


    public DateTime BirthDate { get; set; } // Birth date


    public string City { get; set; } // City
  
    public string District { get; set; }

    public string Address { get; set; } 

    public string PostalCode { get; set; } 
    
    public string Role { get; set; }  // Role of the user, such as 'Customer' or 'Admin'

    public ICollection<ReviewModel> Reviews { get; set; } = new HashSet<ReviewModel>();
    public ICollection<BookingModel> Bookings { get; set; } = new HashSet<BookingModel>();
}
