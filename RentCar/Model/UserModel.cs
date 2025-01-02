using System.Collections.Generic;

namespace RentCar.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class UserModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Id otomatik olarak oluşturulacak
    public int Id { get; set; }

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string EmailAddress { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public string Role { get; set; } = "User"; // Property olarak düzenlendi

    public ICollection<CustomerModel> Customers { get; set; } = new HashSet<CustomerModel>();
}