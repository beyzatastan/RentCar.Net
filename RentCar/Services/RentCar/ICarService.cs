using System.Collections.Generic;
using System.Threading.Tasks;
using RentCar.Model;

namespace RentCar.Services;

public interface ICarService
{
    Task<List<CarModel>>? GetAllCars();
    Task<CarModel>? GetSingleCar(int id);
    Task<List<CarModel>>? AddCar(CarModel car);
    Task<CarModel?> UpdateCar(int id, CarModel request);
    Task<List<CarModel>>? DeleteCar(int id);
}