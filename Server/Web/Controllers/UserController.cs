using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Entity;
using Web.Entity.Context;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        public UserController(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUser()
        {
            return await _dbContext.Users.ToListAsync();
        }

        [HttpGet]
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
        public async Task<ActionResult> AuthenticateUser(User userDto)
        {
            if (userDto == null)
            {
                return BadRequest();
            }

            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userDto.Id);
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
