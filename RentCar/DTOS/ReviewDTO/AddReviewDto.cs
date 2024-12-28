using System;
using System.ComponentModel.DataAnnotations;

namespace RentCar.DTOS.ReviewDTO;
using System.ComponentModel.DataAnnotations;

public class AddReviewDto
{
    [Required]
    public int CustomerId { get; set; }

    [Required]
    public int CarId { get; set; }
    [Required]
    public int SupplierId { get; set; }

    [Required]
    [Range(1, 5)]
    public int Rating { get; set; }

    [StringLength(500)]
    public string Comment { get; set; }
    
    [Required]
    [DataType(DataType.Date)]
    public DateTime? CreateDate { get; set; }

}
