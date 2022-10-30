using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Entity;
using Web.Entity.Context;
using Web.Model;

namespace Web.Controllers
{
    [Route("api")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        public UserController(AppDbContext appDbContext, IMapper mapper)
        {
            _dbContext = appDbContext;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("user")]
        public async Task<ActionResult<List<UserDto>>> GetUser()
        {
            var results = await _dbContext.Users.ToListAsync();
            return Ok(_mapper.Map<List<UserDto>>(results));
        }

        [HttpGet]
        [Route("user/{userId}")]
        public async Task<ActionResult<UserDto>> GetUserById(int userId)
        {
            var result = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<UserDto>(result));
        }

        [HttpPost]
        [Route("user")]
        public async Task<ActionResult> AddUser([FromBody] UserDto userDto)
        {
            if (userDto == null)
            {
                return BadRequest();
            }

            var user = _mapper.Map<User>(userDto);
            await _dbContext.Users.AddAsync(user);
            var result = await _dbContext.SaveChangesAsync();
            if (result > 0)
            {
                return Ok(new
                {
                    Data = _mapper.Map<UserDto>(user)
                });
            }

            return Ok(new
            {
                Message = "Login Success!"
            });
        }

    }
}
