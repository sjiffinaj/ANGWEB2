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
    public class OrderController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        public OrderController(AppDbContext appDbContext, IMapper mapper)
        {
            _dbContext = appDbContext;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("order")]
        public async Task<ActionResult<List<OrderDto>>> GetOrder()
        {
            var results = await _dbContext.Orders.ToListAsync();
            return Ok(_mapper.Map<List<OrderDto>>(results));
        }

        [HttpGet]
        [Route("Order/{OrderId}")]
        public async Task<ActionResult<OrderDto>> GetOrderById(int OrderId)
        {
            var result = await _dbContext.Orders.FirstOrDefaultAsync(x => x.Id == OrderId);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<OrderDto>(result));
        }

        [HttpPost]
        [Route("order")]
        public async Task<ActionResult> AddOrder([FromBody] OrderDto OrderDto)
        {
            if (OrderDto == null)
            {
                return BadRequest();
            }

            var order = _mapper.Map<Order>(OrderDto);
            await _dbContext.Orders.AddAsync(order);
            var result = await _dbContext.SaveChangesAsync();
            if (result > 0)
            {
                return Ok(new
                {
                    Data = _mapper.Map<OrderDto>(order)
                });
            }

            return Ok(new
            {
                Message = "Create failed!"
            });
        }


        [HttpPut]
        [Route("order")]
        public async Task<ActionResult> ModifyOrder([FromBody] OrderDto OrderDto)
        {
            if (OrderDto == null)
            {
                return BadRequest();
            }

            var order = _mapper.Map<Order>(OrderDto);
            _dbContext.Orders.Update(order);
            var result = await _dbContext.SaveChangesAsync();
            if (result > 0)
            {
                return Ok(new
                {
                    Data = _mapper.Map<OrderDto>(order)
                });
            }

            return Ok(new
            {
                Message = "Update failed!"
            });
        }


        [HttpDelete]
        [Route("order")]
        public async Task<ActionResult> DeleteOrder(int OrderId)
        {
            var deletedOrder = _dbContext.Orders.FirstOrDefault(x => x.Id == OrderId);
            if (deletedOrder == null)
            {
                return Ok(new
                {
                    Message = "Order not exist!"
                });
            }
            _dbContext.Orders.Remove(deletedOrder);
            var result = await _dbContext.SaveChangesAsync();
            if (result > 0)
            {
                return Ok(new
                {
                    Message = "Delete success!"
                });
            }

            return Ok(new
            {
                Message = "Delete failed!"
            });
        }
    }
}
