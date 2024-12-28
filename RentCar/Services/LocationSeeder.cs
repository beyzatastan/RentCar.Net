using System.Linq;
using System.Threading.Tasks;
using RentCar.Data;

namespace RentCar.Services;

public class LocationSeeder
{
    private readonly DataContext _context;
    private readonly LocationServices _locationService;

    public LocationSeeder(DataContext context, LocationServices locationService)
    {
        _context = context;
        _locationService = locationService;
    }

    public async Task SeedLocationsAsync()
    {
        // API'den şehir verilerini al
        var locations = await _locationService.GetLocationsFromApi();

        // Eğer veritabanında hiç şehir yoksa, şehirleri ekleyin
        if (!_context.Locations.Any())
        {
            _context.Locations.AddRange(locations);
            await _context.SaveChangesAsync();
        }
    }
}
