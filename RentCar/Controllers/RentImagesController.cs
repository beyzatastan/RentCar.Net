using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentCar.Data;
using RentCar.DTOS;
using RentCar.Model;
using System.Text.Json;
using System.Linq;
using RentCar.DTOS.CarImagesDTO;

namespace RentCar.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RentImagesController : ControllerBase
    {
        private readonly DataContext _context;

        public RentImagesController(DataContext context)
        {
            _context = context;
        }

        [HttpPost("uploadBeforeRentalImages")]
        public async Task<IActionResult> UploadBeforeRentalImages(AddCarImagesDto rentImageDto)
        {
            // Check if all 4 image URLs are provided
            if (string.IsNullOrEmpty(rentImageDto.ImageUrl1) || 
                string.IsNullOrEmpty(rentImageDto.ImageUrl2) || 
                string.IsNullOrEmpty(rentImageDto.ImageUrl3) || 
                string.IsNullOrEmpty(rentImageDto.ImageUrl4))
            {
                return BadRequest("You must upload exactly 4 images after the rental.");
            }

            var booking = await _context.Bookings
                .Include(b => b.Customer)
                .FirstOrDefaultAsync(b => b.Id == rentImageDto.BookingId);

            if (booking == null)
            {
                return NotFound("Booking not found.");
            }

            if (booking.Customer == null)
            {
                return BadRequest("Customer information is missing.");
            }

            // Create the RentImages entity and assign the image URLs
            var rentImages = new RentImages()
            {
                BookingId = booking.Id,
                CarId = booking.CarId,
                CustomerId = booking.CustomerId,
                IsBeforeRental = true, 
                UploadDate = DateTime.UtcNow,
                ImageUrl1 = rentImageDto.ImageUrl1,
                ImageUrl2 = rentImageDto.ImageUrl2,
                ImageUrl3 = rentImageDto.ImageUrl3,
                ImageUrl4 = rentImageDto.ImageUrl4
            };

            // Add the rent images to the context
            _context.RentImages.Add(rentImages);
            await _context.SaveChangesAsync();

            return Ok(new { message = "After rental images uploaded successfully.", bookingId = booking.Id });
        }
        [HttpPost("uploadAfterRentalImages")]
        public async Task<IActionResult> UploadAfterRentalImages(AddCarImagesDto rentImageDto)
        {
            // Check if all 4 image URLs are provided
            if (string.IsNullOrEmpty(rentImageDto.ImageUrl1) || 
                string.IsNullOrEmpty(rentImageDto.ImageUrl2) || 
                string.IsNullOrEmpty(rentImageDto.ImageUrl3) || 
                string.IsNullOrEmpty(rentImageDto.ImageUrl4))
            {
                return BadRequest("You must upload exactly 4 images after the rental.");
            }

            var booking = await _context.Bookings
                .Include(b => b.Customer)
                .FirstOrDefaultAsync(b => b.Id == rentImageDto.BookingId);

            if (booking == null)
            {
                return NotFound("Booking not found.");
            }

            if (booking.Customer == null)
            {
                return BadRequest("Customer information is missing.");
            }

            // Create the RentImages entity and assign the image URLs
            var rentImages = new RentImages()
            {
                BookingId = booking.Id,
                CarId = booking.CarId,
                CustomerId = booking.CustomerId,
                IsBeforeRental = false, // Indicates after rental
                UploadDate = DateTime.UtcNow,
                ImageUrl1 = rentImageDto.ImageUrl1,
                ImageUrl2 = rentImageDto.ImageUrl2,
                ImageUrl3 = rentImageDto.ImageUrl3,
                ImageUrl4 = rentImageDto.ImageUrl4
            };

            // Add the rent images to the context
            _context.RentImages.Add(rentImages);
            await _context.SaveChangesAsync();

            return Ok(new { message = "After rental images uploaded successfully.", bookingId = booking.Id });
        }
        [HttpGet("getBeforeRentalImages/{bookingId}")]
        public async Task<IActionResult> GetBeforeRentalImages(int bookingId)
        {
            var rentImages = await _context.RentImages
                .Where(ri => ri.BookingId == bookingId && ri.IsBeforeRental)
                .FirstOrDefaultAsync();

            if (rentImages == null)
            {
                return NotFound("Before rental images not found for the given booking.");
            }

            // Return the image URLs as a list of strings
            var imageUrls = new List<string>
            {
                rentImages.ImageUrl1,
                rentImages.ImageUrl2,
                rentImages.ImageUrl3,
                rentImages.ImageUrl4
            };

            return Ok(imageUrls);
        }

        [HttpGet("getAfterRentalImages/{bookingId}")]
        public async Task<IActionResult> GetAfterRentalImages(int bookingId)
        {
            var rentImages = await _context.RentImages
                .Where(ri => ri.BookingId == bookingId && !ri.IsBeforeRental)
                .FirstOrDefaultAsync();

            if (rentImages == null)
            {
                return NotFound("After rental images not found for the given booking.");
            }

            // Return the image URLs as a list of strings
            var imageUrls = new List<string>
            {
                rentImages.ImageUrl1,
                rentImages.ImageUrl2,
                rentImages.ImageUrl3,
                rentImages.ImageUrl4
            };

            return Ok(imageUrls);
        }
    }
}
