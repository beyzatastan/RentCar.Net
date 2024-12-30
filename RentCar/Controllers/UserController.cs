using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentCar.Data;
using RentCar.DTOS.UserDTO;
using RentCar.DTOS.LoginDto;
using RentCar.Model;


namespace RentCar.Controllers;
[ApiController]
[Route("api/[controller]")]
public class UserController: ControllerBase{
    
        private readonly DataContext _context;

        public UserController(DataContext context)
        {
            _context = context;
        }
        
        [HttpGet("getAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _context.Users.ToListAsync();
            return Ok(users);
        }
        
        [HttpGet("getUserById/{userId}")]
        public async Task<ActionResult<UserModel>> GetUserById(int userId)
        {
            var user = await _context.Users
                .Include(u => u.Customers)
                .FirstOrDefaultAsync(u => u.Id == userId);


            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // POST: api/Customer
        [HttpPost("addUser")]
        public async Task<IActionResult> AddUser([FromBody] AddUserDto dto)
        {
            if (dto == null)
            {
                return BadRequest("User data is required.");
            }

            var user = new UserModel()
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                EmailAddress = dto.EmailAddress,
                PhoneNumber = dto.PhoneNumber,
                Password =  dto.Password,
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "User added successfully", UserId = user.Id });
        }
        
        
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.PhoneNumber == dto.PhoneNumber && u.Password == dto.Password);

            if (user == null)
            {
                return Unauthorized("Geçersiz telefon numarası veya şifre.");
            }

            return Ok(new { Message = "Giriş başarılı", UserId = user.Id });
        }

        
        // PUT: api/Customer/{id}
        [HttpPut("updateUserById/{userId}")]
        public async Task<IActionResult> UpdateUser(int userId ,[FromBody] UserModel user)
        {
            if (userId != user.Id)
            {
                return BadRequest();
            }

            var existingUser = await _context.Users.FindAsync(userId);
            if (existingUser == null)
            {
                return NotFound();
            }

            // Update customer details
            existingUser.PhoneNumber = user.PhoneNumber;
            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.EmailAddress = user.EmailAddress;
            existingUser.Password = user.Password;
           


            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Customer/{id}
        [HttpDelete("deleteUserById/{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
