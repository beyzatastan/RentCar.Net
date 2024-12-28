using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RentCar.DTOS;
using RentCar.Model;
using RentCar.Data;

namespace RentCar.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LocationController : ControllerBase
{
    private readonly DataContext _context;

    public LocationController(DataContext context)
    {
        _context = context;
    }

    // POST: api/Location
    [HttpPost("addLocation")]
    public IActionResult AddLocation([FromBody] LocationDto locationDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var location = new LocationModel
        {
            City = locationDto.City, // Default value if no Country property exists in DTO
        };

        _context.Locations.Add(location);
        _context.SaveChanges();

        return CreatedAtAction(nameof(AddLocation), new { id = location.Id }, location);
    }

    // GET: api/Location/{id}
    [HttpGet("getLocationById/{locationId}")]
    public IActionResult GetLocation(int locationId)
    {
        var locations = _context.Locations.Find(locationId);
        if (locations == null)
        {
            return NotFound();
        }

        return Ok(new
        {
            locations.City,
        });
    }

// GET: api/Location
    [HttpGet("getAllLocations")]
    public IActionResult GetAllLocations()
    {
        var locations = _context.Locations.ToList(); // Tüm Locations'ları getir

        if (locations == null || !locations.Any())
        {
            return NotFound("Hiçbir şehir kaydı bulunamadı.");
        }

        return Ok(locations); // Locations verilerini döndür
    }
}