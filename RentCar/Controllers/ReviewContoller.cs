using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentCar.Data;
using RentCar.DTOS.ReviewDTO;
using RentCar.Model;

namespace RentCar.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ReviewContoller:ControllerBase
{
    
    private readonly DataContext _context;

    public ReviewContoller(DataContext context)
    {
        _context = context;
    }

    [HttpGet("getAllReviews")]
    public async Task<IActionResult> GetAllReviews()
    {
        var reviews = await _context.Reviews
            .Include(r => r.Customer)
            .Include(r => r.Car)
            .ToListAsync();

        return Ok(reviews);
    }
    [HttpGet("getReviewsByCarId/{carId}")]
    public async Task<IActionResult> GetReviewsForCar(int carId)
    {
        var reviews = await _context.Reviews
            .Where(r => r.CarId == carId)
            .Include(r => r.Customer)
            .Select(r => new ReviewDto
            {
                Id = r.Id,
                CustomerId = r.CustomerId,
                CustomerName = r.Customer != null ? $"{r.Customer.IdentityNumber}" : null,
                SupplierId = r.SupplierId,
                SupplierName = r.Supplier != null ? $"{r.Supplier.CompanyName}" : null,
                CarId = r.CarId,
                Rating = r.Rating,
                Comment = r.Comment,
                CreateDate = DateTime.UtcNow
            })
            .ToListAsync();

        if (!reviews.Any())
            return NotFound("No reviews found for this car");

        return Ok(reviews);
    }

    [HttpPost("addReview")]
    public async Task<IActionResult> AddReview(AddReviewDto dto)
    {
        var customer = await _context.Customers.FindAsync(dto.CustomerId);
        var car = await _context.Cars.FindAsync(dto.CarId);

        if (customer == null || car == null)
            return NotFound("Customer or Car not found");

        var review = new ReviewModel
        {
            CustomerId = dto.CustomerId,
            CarId = dto.CarId,
            SupplierId = dto.SupplierId,
            Rating = dto.Rating,
            Comment = dto.Comment, 
            DateCreated = dto.CreateDate ?? DateTime.UtcNow
        };

        _context.Reviews.Add(review);
        await _context.SaveChangesAsync();

        return Ok("User review successfully");
    }
    [HttpDelete("deleteReviewById/{reviewId}")]
    public async Task<IActionResult> DeleteReview(int id)
    {
        var review = await _context.Reviews.FindAsync(id);
        if (review == null) return NotFound("Review not found");

        _context.Reviews.Remove(review);
        await _context.SaveChangesAsync();

        return NoContent();
    }

}