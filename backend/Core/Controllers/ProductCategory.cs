using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public ProductCategory(CoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateCategory(CreateProductCategoryDto model)
        {
            return null;
        }
    }
}