using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Data;
using Core.DTOs;
using Microsoft.AspNetCore.Mvc;

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
    }
}