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
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateCategory(CreateProductCategoryDto model)
        {
            var category = _mapper.Map<Core.Entities.ProductCategory>(model);
            category.CreatedDate = DateTime.Now;

            await _dbContext.ProductCategories.AddAsync(category);

            var result = _dbContext.SaveChangesAsync();

            if (result.IsCompletedSuccessfully) return StatusCode(201);
            else return BadRequest();
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<ApiResponse<List<GetProductCategoryDto>>>> GetAll()
        {
            var result = await _dbContext.ProductCategories.ToListAsync();

            if (result == null)
                return BadRequest(ApiResponse<List<GetProductCategoryDto>>.Error("Error Occurred"));

            if (result.Count == 0)
                return NotFound(ApiResponse<List<GetProductCategoryDto>>.NotFound());

            var dto = _mapper.Map<List<GetProductCategoryDto>>(result);

            return ApiResponse<List<GetProductCategoryDto>>.Success(dto);
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<ActionResult<ApiResponse<GetProductCategoryDto>>> GetById(int id)
        {
            var result = await _dbContext.ProductCategories.FindAsync(id);

            if (result == null)
                return NotFound(ApiResponse<GetProductCategoryDto>.NotFound());

            var dto = _mapper.Map<GetProductCategoryDto>(result);

            return ApiResponse<GetProductCategoryDto>.Success(dto);
        }
    }
}