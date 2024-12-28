using System;
using System.ComponentModel.DataAnnotations;
using DataType = Swashbuckle.AspNetCore.SwaggerGen.DataType;

public class ReviewDto
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string CustomerName { get; set; } // Eğer müşteri adını göstermek istiyorsanız
    public int SupplierId { get; set; }
    public string SupplierName { get; set; } 
    public int CarId { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; }
    
    public DateTime CreateDate { get; set; }
}