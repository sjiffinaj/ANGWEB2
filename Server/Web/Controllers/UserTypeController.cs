using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Entity.Context;
using Web.Model;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTypeController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        public UserTypeController(AppDbContext appDbContext, IMapper mapper)
        {
            _dbContext = appDbContext;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("usertype")]
        public async Task<ActionResult<List<UserTypeDto>>> GetUserType()
        {
            var results = await _dbContext.UserTypes.ToListAsync();
            return Ok(_mapper.Map<List<UserTypeDto>>(results));
        }
    }
}
