using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Data;
using Core.DTOs;
using Core.Helpers;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Core.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductCategory : ControllerBase
    {
        private readonly CoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public ProductCategory(CoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(typeof(ApiResponse<string>), 201)]
        public async Task<IActionResult> CreateCategory(CreateProductCategoryDto model)
        {
            var category = _mapper.Map<Core.Entities.ProductCategory>(model);
            category.CreatedDate = DateTime.Now;

            await _dbContext.ProductCategories.AddAsync(category);

            var result = await _dbContext.SaveChangesAsync();

            if (result > 0)
            {
                return Ok(ApiResponse<string>.Success());
            }
            else
            {
                return BadRequest(ApiResponse<string>.Error());
            }
        }

        [HttpPut]
        [Route("Update/{id}")]
        [ProducesResponseType(typeof(ApiResponse<string>), 200)]
        public async Task<IActionResult> Update(int id, UpdateProductCategoryDto model)
        {
            var existingCategory = await _dbContext.ProductCategories.FindAsync(id);

            if (existingCategory == null)
                return NotFound(ApiResponse<string>.NotFound());

            existingCategory.CategoryName = model.CategoryName;
            existingCategory.ModifiedBy = "admin";
            existingCategory.ModifiedDate = DateTime.Now;

            var result = await _dbContext.SaveChangesAsync();

            if (result > 0)
                return Ok(ApiResponse<string>.Success());
            else
                return BadRequest(ApiResponse<string>.Error());
        }

        [HttpGet]
        [Route("GetAll")]
        [ProducesResponseType(typeof(ApiResponse<List<GetProductCategoryDto>>), 200)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _dbContext.ProductCategories.ToListAsync();

            if (result == null)
                return BadRequest(ApiResponse<string>.Error("Error Occurred"));

            if (result.Count() == 0)
                return NotFound(ApiResponse<string>.NotFound());

            var dto = _mapper.Map<List<GetProductCategoryDto>>(result);

            var response = ApiResponse<List<GetProductCategoryDto>>.Success(dto);
            return Ok(response);
        }

        [HttpGet]
        [Route("GetById/{id}")]
        [ProducesResponseType(typeof(ApiResponse<GetProductCategoryDto>), 200)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _dbContext.ProductCategories.FindAsync(id);

            if (result == null)
                return NotFound(ApiResponse<GetProductCategoryDto>.NotFound());

            var dto = _mapper.Map<GetProductCategoryDto>(result);

            var response = ApiResponse<GetProductCategoryDto>.Success(dto);
            return Ok(response);
        }
    }
}