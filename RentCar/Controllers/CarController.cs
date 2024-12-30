using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentCar.CarDTO;
using RentCar.Data;
using RentCar.Model;
using RentCar.Services;

namespace RentCar.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarController : ControllerBase
{
    private readonly ICarService _carService;
    private readonly DataContext _context;

    public CarController(ICarService carService, DataContext context)
    {
        _carService = carService;
        _context = context;
    }
    [HttpPost("addCar")]
    public async Task<IActionResult> AddCar([FromBody] AddCarDto dto)
    {
        // Yeni araba nesnesi oluşturuluyor
        // Yeni araba nesnesi oluşturuluyor
        var car = new CarModel
        {
            Brand = dto.Brand,
            Model = dto.Model,
            Year = dto.Year,
            LicensePlate = dto.LicensePlate,
            TransmissionType = dto.TransmissionType,
            SeatCount = dto.SeatCount,
            DailyPrice = dto.DailyPrice,
            SupplierId = dto.SupplierId,
            LocationId = dto.LocationId,
            GasType = dto.GasType,
            CarClass = dto.CarClass,
            Deposit = dto.Deposit,
            ImageUrl = dto.Images != null && dto.Images.Count > 0 
                ? Path.Combine("images", dto.Images.First().FileName) 
                : null // Set default or null value for ImageUrl
        };

// Eğer resimler varsa, işlemler yapılabilir
        if (dto.Images != null && dto.Images.Count > 0)
        {
            foreach (var image in dto.Images)
            {
                // Resmi kaydetme işlemi burada yapılabilir
                var filePath = Path.Combine("images", image.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }
            }
        }


        // Araba veritabanına ekleniyor
        _context.Cars.Add(car);
        await _context.SaveChangesAsync();

        return Ok("Car added successfully");
    }

    [HttpGet("getCars")]
    public async Task<IActionResult> GetCars()
    {
        var cars = await _context.Cars.ToListAsync();
        return Ok(cars);
    }

    [HttpDelete("deleteCarById/{carId}")]
    public async Task<IActionResult> DeleteCar(int carId)
    {
        var car = await _context.Cars
            .Include(c => c.Bookings) // Include the related bookings
            .FirstOrDefaultAsync(c => c.Id == carId);

        if (car == null)
        {
            return NotFound();
        }

        // Delete all related bookings first
        _context.Bookings.RemoveRange(car.Bookings);
        await _context.SaveChangesAsync();

        // Now delete the car
        _context.Cars.Remove(car);
        await _context.SaveChangesAsync();

        return NoContent(); // or another response as needed
    }

    [HttpGet("getCarById/{id}")]
    public async Task<IActionResult> GetCar(int id)
    {
        var car = await _context.Cars
            .Include(c => c.Bookings)  // İlişkili Booking verisini yükle
            .Include(c => c.Reviews)   // İlişkili Review verisini yükle// İlişkili Image verisini yükle
            .FirstOrDefaultAsync(c => c.Id == id);

        if (car == null)
        {
            return NotFound();
        }

        var carDto = new
        {
            car.Id,
            car.Brand,
            car.Model,
            car.Year,
            car.LicensePlate,
            car.TransmissionType,
            car.LocationId,
            car.ImageUrl,
            car.SeatCount,
            car.DailyPrice,
            car.Supplier,
            car.SupplierId,
            car.GasType,
            car.Deposit,
            car.CarClass,
            Bookings = car.Bookings != null && car.Bookings.Any() 
                ? car.Bookings.Select(b => new 
                {
                    b.Id,
                    b.CarId,
                    b.StartDate,
                    b.EndDate
                }).ToList() 
                : null,
            Reviews = car.Reviews != null && car.Reviews.Any() 
                ? car.Reviews.Select(r => new 
                {
                    r.Id,
                    r.Rating,
                    r.Comment,
                    r.DateCreated
                }).ToList() 
                : null,
        };

        return Ok(carDto);
    }
    [HttpGet("getCarsByUserId/{userId}")]
    public async Task<IActionResult> GetCarsByUserId(int userId)
    {
        // Kullanıcının yaptığı booking'lere bağlı araçları getir
        var cars = await _context.Cars
            .Include(c => c.Bookings) // İlişkili Booking verisini yükle
            .Include(c => c.Reviews)  // İlişkili Review verisini yükle
            .Where(c => c.Bookings.Any(b => b.Customer.UserId == userId)) // Kullanıcının ID'sine göre filtreleme
            .ToListAsync();

        if (!cars.Any())
        {
            return NotFound($"No cars found for user with ID {userId}");
        }

        var carDtos = cars.Select(car => new
        {
            car.Id,
            car.Brand,
            car.Model,
            car.Year,
            car.LicensePlate,
            car.TransmissionType,
            car.SeatCount,
            car.DailyPrice,
            car.Supplier,
            car.SupplierId,
            car.GasType,
            car.Deposit,
            car.CarClass,
            Bookings = car.Bookings != null && car.Bookings.Any()
                ? car.Bookings.Select(b => new
                {
                    b.Id,
                    b.CarId,
                    b.StartDate,
                    b.EndDate
                }).ToList()
                : null,
            Reviews = car.Reviews != null && car.Reviews.Any()
                ? car.Reviews.Select(r => new
                {
                    r.Id,
                    r.Rating,
                    r.Comment,
                    r.DateCreated
                }).ToList()
                : null,
        }).ToList();

        return Ok(carDtos);
    }


    // PUT: api/car/{id}
    [HttpPut("updateCarById/{carId}")]
    public async Task<IActionResult> UpdateCar(int id, UpdateCarDto dto)
    {
        // ID eşleşmesini kontrol et
        if (id != dto.Id)
        {
            return BadRequest("ID mismatch");
        }

        // Veritabanında arabayı bul
        var car = await _context.Cars.FindAsync(id);
        if (car == null)
        {
            return NotFound("Car not found");
        }

        // Gönderilen DTO'daki alanlarla arabayı güncelle
        if (!string.IsNullOrEmpty(dto.Brand)) car.Brand = dto.Brand;
        if (!string.IsNullOrEmpty(dto.Model)) car.Model = dto.Model;
        if (dto.Year.HasValue) car.Year = dto.Year.Value;
        if (!string.IsNullOrEmpty(dto.LicensePlate)) car.LicensePlate = dto.LicensePlate;
        if (!string.IsNullOrEmpty(dto.TransmissionType)) car.TransmissionType = dto.TransmissionType;
        if (dto.SeatCount.HasValue) car.SeatCount = dto.SeatCount.Value;
        if (dto.DailyPrice.HasValue) car.DailyPrice = dto.DailyPrice.Value;
        if (dto.SupplierId.HasValue) car.SupplierId = dto.SupplierId.Value;
        if (dto.LocationId.HasValue) car.LocationId = dto.LocationId.Value;
        if (!string.IsNullOrEmpty(dto.GasType)) car.GasType = dto.GasType;
        if (!string.IsNullOrEmpty(dto.CarClass)) car.CarClass = dto.CarClass;
//depozito değeri yok 
        // Değişiklikleri kaydet
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
}