using AutoMapper;
using Core.Data;
using Core.DTOs;
using Core.Entities;
using Core.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly CoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public WarehouseController(CoreDbContext coreDbContext, IMapper mapper)
        {
            _mapper = mapper;
            _dbContext = coreDbContext;
        }

        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(typeof(ApiResponse<string>), 201)]
        public async Task<IActionResult> Create(WarehouseDto model)
        {
            var warehouse = _mapper.Map<Warehouse>(model);
            warehouse.CreatedBy = Utility.GetUsername();
            warehouse.CreatedDate = DateTime.Now;

            await _dbContext.Warehouses.AddAsync(warehouse);

            var result = await _dbContext.SaveChangesAsync();

            if (result > 0)
                return Created("", ApiResponse<string>.Success());
            else
                return BadRequest(ApiResponse<string>.Error());
        }

        [HttpPut]
        [Route("Update/{id}")]
        [ProducesResponseType(typeof(ApiResponse<string>), 200)]
        public async Task<IActionResult> Update(int id, WarehouseDto model)
        {
            var existingWarehouse = await _dbContext.Warehouses.FindAsync(id);

            if (existingWarehouse == null)
                return NotFound(ApiResponse<string>.NotFound());

            existingWarehouse.Name = model.Name;
            existingWarehouse.Description = model.Description;
            existingWarehouse.IsActive = model.IsActive;
            existingWarehouse.ModifiedBy = Utility.GetUsername();
            existingWarehouse.ModifiedDate = DateTime.Now;

            var result = await _dbContext.SaveChangesAsync();

            if (result > 0)
                return Ok(ApiResponse<string>.Success());
            else
                return BadRequest(ApiResponse<string>.Error());
        }

        [HttpGet]
        [Route("GetAll")]
        [ProducesResponseType(typeof(ApiResponse<List<Supplier>>), 200)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _dbContext.Warehouses.Include(w => w.Stores).ToListAsync();

            if (result == null)
                return BadRequest(ApiResponse<string>.Error("Error Occurred"));

            if (result.Count() == 0)
                return NotFound(ApiResponse<string>.NotFound());

            var response = ApiResponse<List<Warehouse>>.Success(result);

            return Ok(response);
        }

        [HttpGet]
        [Route("GetById/{id}")]
        [ProducesResponseType(typeof(ApiResponse<Warehouse>), 200)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _dbContext.Warehouses.Include(w => w.Stores).FirstOrDefaultAsync(x => x.Id== id);

            if (result == null)
                return NotFound(ApiResponse<string>.NotFound());

            return Ok(ApiResponse<Warehouse>.Success(result));
        }
    }
}
