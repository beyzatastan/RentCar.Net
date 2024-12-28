using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentCar.Data;
using RentCar.DTOS.BookingDTO;
using RentCar.Model;
using System.Linq;

namespace RentCar.Controllers;
[ApiController]
[Route("api/[controller]")]
public class BookingsContoller:ControllerBase

{
        private readonly DataContext _context;

        public BookingsContoller(DataContext context)
        {
            _context = context;
        }
        [HttpGet("getBookingById/{bookingId}")]
        public async Task<ActionResult<BookingModel>> GetBooking(int bookingId)
        {
            var booking = await _context.Bookings
                .Include(c => c.Car)   // Reviews ilişkisini yükle
                .Include(c => c.Customer)  // Bookings ilişkisini yükle
                .FirstOrDefaultAsync(c => c.Id == bookingId);

            if (booking == null)
            {
                return NotFound();
            }

            return Ok(booking);
        }


        [HttpPost("addBooking")]
        public async Task<IActionResult> CreateBooking([FromBody] AddBookingDto dto)
        {
            var car = await _context.Cars.FindAsync(dto.CarId);
            if (car == null) return NotFound("Car not found");
            

            // Rezervasyon modelini oluşturuyoruz
            var booking = new BookingModel
            {
                CustomerId = dto.CustomerId,
                CarId = dto.CarId,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                StartLocationId = dto.StartLocationId,
                EndLocationId = dto.EndLocationId,
            };

            // Rezervasyonu veritabanına ekliyoruz
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            // Müşteri ID'sini geri döndürüyoruz
            return Ok(new { message = "Booking created successfully", bookingId = booking.Id });
        }


        [HttpGet("getBookings")]
        public async Task<IActionResult> GetBookings()
        {
            var bookings = await _context.Bookings
                .Include(b => b.Car)
                .Include(b => b.Customer)
                .ToListAsync();

            return Ok(bookings);
        }
        [HttpGet("getBookingsByCar/{carId}")]
        public async Task<IActionResult> GetBookingsByCar(int carId)
        {
            var bookings = await _context.Bookings
                .Where(b => b.CarId == carId)
                .Include(b => b.Car)
                .Include(b => b.Customer)
                .Include(b => b.StartLocation)  // Başlangıç lokasyonunu ekle
                .Include(b => b.EndLocation)    // Bitiş lokasyonunu ekle
                .Select(b => new BookingDto
                {
                    BookingId = b.Id,
                    CustomerId = b.CustomerId,
                    CarId = b.CarId,
                    CarModel = b.Car.Model,
                    StartDate = b.StartDate,
                    EndDate = b.EndDate,
                    
                })
                .ToListAsync();

            if (!bookings.Any())
            {
                return NotFound($"No bookings found for car with ID {carId}");
            }

            return Ok(bookings);
        }
        [HttpGet("getBookingsByCustomer/{customerId}")]
        public async Task<IActionResult> GetBookingsByCustomer(int customerId)
        {
            var bookings = await _context.Bookings
                .Where(b => b.CustomerId == customerId) // CustomerId'ye göre filtreleme
                .Include(b => b.Car)
                .Include(b => b.Customer)
                .Include(b => b.StartLocation) // Başlangıç lokasyonu
                .Include(b => b.EndLocation)   // Bitiş lokasyonu
                .Select(b => new BookingDto
                {
                    BookingId = b.Id,
                    CustomerId = b.CustomerId,
                    CarId = b.CarId,
                    CarModel = b.Car.Model,
                    StartLocation = b.StartLocation.City, // veya istediğiniz alan
                    EndLocation = b.EndLocation.City,     // veya istediğiniz alan
                    StartDate = b.StartDate,
                    EndDate = b.EndDate,
                })
                .ToListAsync();

            if (!bookings.Any())
            {
                return NotFound($"No bookings found for customer with ID {customerId}");
            }

            return Ok(bookings);
        }


    }

 