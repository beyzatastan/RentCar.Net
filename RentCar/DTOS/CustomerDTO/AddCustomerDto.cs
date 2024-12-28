using System;
using System.ComponentModel.DataAnnotations;

namespace RentCar.DTOS.CustomerDTO;

public class AddCustomerDto
{
   public string FirstName { get; set; } = string.Empty; // User's first name

    public string LastName { get; set; } = string.Empty; // User's last name
 
    public string Email { get; set; } = string.Empty; // User's email address for login and communication


    [StringLength(11, ErrorMessage = "Identity Number must be 11 characters.")]
    public string IdentityNumber { get; set; } // T.C. Kimlik No

    public DateTime DrivingLicenseIssuedDate { get; set; } // Ehliyet Veriliş Tarihi

    public string DrivingLicenseNumber { get; set; } // Ehliyet Numarası

 
    public DateTime BirthDate { get; set; } // Doğum Tarihi
    
    public string PhoneNumber { get; set; } // Telefon Numarası

    [StringLength(100, ErrorMessage = "City name is too long.")]
    public string City { get; set; } // Şehir

    [ StringLength(100, ErrorMessage = "District name is too long.")]
    public string District { get; set; } // İlçe

    [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters.")]
    public string Address { get; set; } = string.Empty;  // Adres

    [ StringLength(10, ErrorMessage = "Postal Code cannot exceed 10 characters.")]
    public string PostalCode { get; set; } // Posta Kodu
}