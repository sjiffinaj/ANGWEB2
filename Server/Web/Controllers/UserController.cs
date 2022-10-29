using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Entity;
using Web.Entity.Context;

namespace Web.Controllers
{
    [Route("api")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        public UserController(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }

        [HttpGet]
        [Route("user")]
        public async Task<ActionResult<List<User>>> GetUser()
        {
            return await _dbContext.Users.ToListAsync();
        }

        [HttpGet]
        [Route("user/{userId}")]
        public async Task<ActionResult<User>> GetUserById(int userId)
        {
            var result = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        [Route("user")]
        public async Task<ActionResult> AddUser([FromBody] User userDto)
        {
            if (userDto == null)
            {
                return BadRequest();
            }

            await _dbContext.Users.AddAsync(userDto);
            var result = await _dbContext.SaveChangesAsync();
            if (result > 0)
            {
                return Ok(new
                {
                    Data = userDto
                });
            }

            return Ok(new
            {
                Message = "Login Success!"
            });
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> AuthenticateUser([FromBody] User userDto)
        {
            if (userDto == null)
            {
                return BadRequest();
            }

            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Username == userDto.Username && x.Password == userDto.Password);
            if (user == null)
            {
                return NotFound(new
                {
                    Message = "User Not Found!"
                });
            }

            return Ok(new
            {
                Message = "Login Success!"
            });
        }
    }
}
