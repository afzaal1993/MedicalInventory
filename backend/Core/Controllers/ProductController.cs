using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Data;
using Core.DTOs;
using Core.Entities;
using Core.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Core.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly CoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public ProductController(CoreDbContext coreDbContext, IMapper mapper)
        {
            _mapper = mapper;
            _dbContext = coreDbContext;
        }

        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(typeof(ApiResponse<string>), 201)]
        public async Task<IActionResult> Create(ProductDto model)
        {
            var product = _mapper.Map<Product>(model);

            product.CreatedBy = Utility.GetUsername();
            product.CreatedDate = DateTime.Now;

            await _dbContext.Products.AddAsync(product);

            var result = await _dbContext.SaveChangesAsync();

            if (result > 0)
                return Created("", ApiResponse<string>.Success());
            else
                return BadRequest(ApiResponse<string>.Error());
        }

        [HttpPut]
        [Route("Update/{id}")]
        [ProducesResponseType(typeof(ApiResponse<string>), 200)]
        public async Task<IActionResult> Update(int id, ProductDto model)
        {
            var existingProduct = await _dbContext.Products.FindAsync(id);

            if (existingProduct == null)
                return NotFound(ApiResponse<string>.NotFound());

            existingProduct.Name = model.Name;
            existingProduct.Description = model.Description;
            existingProduct.MinimumStockLevel = model.MinimumStockLevel;
            existingProduct.ProductCategoryId = model.ProductCategoryId;
            existingProduct.IsActive = model.IsActive;
            existingProduct.ModifiedBy = Utility.GetUsername();
            existingProduct.ModifiedDate = DateTime.Now;

            var result = await _dbContext.SaveChangesAsync();

            if (result > 0)
                return Ok(ApiResponse<string>.Success());
            else
                return BadRequest(ApiResponse<string>.Error());
        }

        [HttpGet]
        [Route("GetAll")]
        [ProducesResponseType(typeof(ApiResponse<List<GetProductDto>>), 200)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _dbContext.Products.Include(p => p.ProductCategory).ToListAsync();

            if (result == null)
                return BadRequest(ApiResponse<string>.Error("Error Occurred"));

            if (result.Count() == 0)
                return NotFound(ApiResponse<string>.NotFound());

            var productDtos = _mapper.Map<List<GetProductDto>>(result);

            return Ok(ApiResponse<List<GetProductDto>>.Success(productDtos));
        }

        [HttpGet]
        [Route("GetById/{id}")]
        [ProducesResponseType(typeof(ApiResponse<GetProductDto>), 200)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _dbContext.Products.Include(p => p.ProductCategory).FirstOrDefaultAsync(x => x.Id == id);

            if (result == null)
                return NotFound(ApiResponse<string>.NotFound());

            var productDto = _mapper.Map<GetProductDto>(result);

            return Ok(ApiResponse<GetProductDto>.Success(productDto));
        }
    }
}