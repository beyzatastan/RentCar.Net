using RentCar.Model;

namespace RentCar.Services;

using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

public class LocationServices
{
    private readonly HttpClient _httpClient;

    public LocationServices(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<LocationModel>> GetLocationsFromApi()
    {
        var response = await _httpClient.GetStringAsync("https://turkiyeapi.dev/api/v1/provinces");
        
        // JSON verisini deserialize et
        var locations = JsonSerializer.Deserialize<List<LocationModel>>(response);

        return locations;
    }
}
