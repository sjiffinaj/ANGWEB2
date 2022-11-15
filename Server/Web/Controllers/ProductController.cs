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
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        public ProductController(AppDbContext appDbContext, IMapper mapper)
        {
            _dbContext = appDbContext;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("product")]
        public async Task<ActionResult<List<ProductDto>>> GetProduct()
        {
            var results = await _dbContext.Products.ToListAsync();
            return Ok(_mapper.Map<List<ProductDto>>(results));
        }

        [HttpGet]
        [Route("Product/{ProductId}")]
        public async Task<ActionResult<ProductDto>> GetProductById(int ProductId)
        {
            var result = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == ProductId);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ProductDto>(result));
        }

        [HttpPost]
        [Route("product")]
        public async Task<ActionResult> AddProduct([FromBody] ProductDto ProductDto)
        {
            if (ProductDto == null)
            {
                return BadRequest();
            }

            var Product = _mapper.Map<Product>(ProductDto);
            await _dbContext.Products.AddAsync(Product);
            var result = await _dbContext.SaveChangesAsync();
            if (result > 0)
            {
                return Ok(new
                {
                    Data = _mapper.Map<ProductDto>(Product)
                });
            }

            return Ok(new
            {
                Message = "Create failed!"
            });
        }


        [HttpPut]
        [Route("product")]
        public async Task<ActionResult> ModifyProduct([FromBody] ProductDto ProductDto)
        {
            if (ProductDto == null)
            {
                return BadRequest();
            }

            var product = _mapper.Map<Product>(ProductDto);
            _dbContext.Products.Update(product);
            var result = await _dbContext.SaveChangesAsync();
            if (result > 0)
            {
                return Ok(new
                {
                    Data = _mapper.Map<ProductDto>(product)
                });
            }

            return Ok(new
            {
                Message = "Update failed!"
            });
        }


        [HttpDelete]
        [Route("product")]
        public async Task<ActionResult> DeleteProduct(int ProductId)
        {
            var deletedProduct = _dbContext.Products.FirstOrDefault(x => x.Id == ProductId);
            if (deletedProduct == null)
            {
                return Ok(new
                {
                    Message = "Product not exist!"
                });
            }
            _dbContext.Products.Remove(deletedProduct);
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
