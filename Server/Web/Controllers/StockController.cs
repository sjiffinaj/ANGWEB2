using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Entity.Context;
using Web.Entity;
using Web.Model;
using Microsoft.EntityFrameworkCore;

namespace Web.Controllers
{
    [Route("api")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        public StockController(AppDbContext appDbContext, IMapper mapper)
        {
            _dbContext = appDbContext;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("stock")]
        public async Task<ActionResult<List<StockDto>>> GetStock()
        {
            var results = await _dbContext.Stocks.ToListAsync();
            return Ok(_mapper.Map<List<StockDto>>(results));
        }

        [HttpGet]
        [Route("Stock/{StockId}")]
        public async Task<ActionResult<StockDto>> GetStockById(int StockId)
        {
            var result = await _dbContext.Stocks.FirstOrDefaultAsync(x => x.Id == StockId);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<StockDto>(result));
        }

        [HttpPost]
        [Route("stock")]
        public async Task<ActionResult> AddStock([FromBody] StockDto StockDto)
        {
            if (StockDto == null)
            {
                return BadRequest();
            }

            var stock = _mapper.Map<Stock>(StockDto);
            await _dbContext.Stocks.AddAsync(stock);
            var result = await _dbContext.SaveChangesAsync();
            if (result > 0)
            {
                return Ok(new
                {
                    Data = _mapper.Map<StockDto>(stock)
                });
            }

            return Ok(new
            {
                Message = "Create failed!"
            });
        }


        [HttpPut]
        [Route("stock")]
        public async Task<ActionResult> ModifyStock([FromBody] StockDto StockDto)
        {
            if (StockDto == null)
            {
                return BadRequest();
            }

            var stock = _mapper.Map<Stock>(StockDto);
            _dbContext.Stocks.Update(stock);
            var result = await _dbContext.SaveChangesAsync();
            if (result > 0)
            {
                return Ok(new
                {
                    Data = _mapper.Map<StockDto>(stock)
                });
            }

            return Ok(new
            {
                Message = "Update failed!"
            });
        }


        [HttpDelete]
        [Route("stock")]
        public async Task<ActionResult> DeleteStock(int StockId)
        {
            var deletedStock = _dbContext.Stocks.FirstOrDefault(x => x.Id == StockId);
            if (deletedStock == null)
            {
                return Ok(new
                {
                    Message = "Stock not exist!"
                });
            }
            _dbContext.Stocks.Remove(deletedStock);
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
