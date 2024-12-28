using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RentCar.Data;
using RentCar.Model;

namespace RentCar.Services.Car;

public class CarService:ICarService
{
    private readonly DataContext _context;

    public CarService(DataContext context)
    {
        _context = context;
    }
    public async Task<List<CarModel>>? GetAllCars()
    {
        var cars = await _context.Cars.ToListAsync();
        return cars;
    }

    public async Task<CarModel>? GetSingleCar(int id)
    {
        var car =  await _context.Cars.FindAsync(id);
        if (car == null)
            return null;
        return car;
    }
    public async Task<List<CarModel>>? AddCar(CarModel car)
    {
        _context.Cars.Add(car);
        await _context.SaveChangesAsync();
        return await _context.Cars.ToListAsync();
    }

  

    public async Task<CarModel?> UpdateCar(int id, CarModel request)
    {
        var car = await _context.Cars.FindAsync(id);
        if (car is null)
            return null; // Eğer araç bulunamazsa null döndür

        // Primitive properties güncellemesi
        car.Brand = request.Brand;
        car.Model = request.Model;
        car.Year = request.Year;
        car.LicensePlate = request.LicensePlate;
        car.TransmissionType = request.TransmissionType;
        car.SeatCount = request.SeatCount;
        car.DailyPrice = request.DailyPrice;

        // Supplier güncellemesi
        car.SupplierId = request.SupplierId;
        if (request.Supplier != null)
        {
            car.Supplier = request.Supplier;
        }

        // Location güncellemesi
        car.LocationId = request.LocationId;
        if (request.Location != null)
        {
            car.Location = request.Location;
        }

        // Bookings güncellemesi
        if (request.Bookings != null)
        {
            car.Bookings.Clear();
            foreach (var booking in request.Bookings)
            {
                car.Bookings.Add(booking);
            }
        }

        // Değişiklikleri kaydet
        await _context.SaveChangesAsync();

        return car; 
    }


    public  async Task<List<CarModel>>?  DeleteCar(int id)
    {
        var car =  await _context.Cars.FindAsync(id);
        if (car is null)
            return null;
           
        _context.Cars.Remove(car);
        await _context.SaveChangesAsync();
        return await _context.Cars.ToListAsync();
    }
}