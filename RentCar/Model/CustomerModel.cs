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
    public int Id { get; set; }

    [Required, ForeignKey("User")]
    public int UserId { get; set; } // Foreign key
    public UserModel User { get; set; } // Navigation property

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    [StringLength(11)]
    public string IdentityNumber { get; set; }

    public DateTime DrivingLicenseIssuedDate { get; set; }

    [StringLength(20)]
    public string DrivingLicenseNumber { get; set; }

    public DateTime BirthDate { get; set; }

    public string City { get; set; } = string.Empty;
    public string District { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;

    public string Role { get; set; } = "Customer"; // Property olarak düzenlendi

    public ICollection<ReviewModel> Reviews { get; set; } = new HashSet<ReviewModel>();
    public ICollection<BookingModel> Bookings { get; set; } = new HashSet<BookingModel>();
}