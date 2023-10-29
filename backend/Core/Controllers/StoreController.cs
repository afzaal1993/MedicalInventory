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
    public class StoreController : ControllerBase
    {
        private readonly CoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public StoreController(CoreDbContext coreDbContext, IMapper mapper)
        {
            _mapper = mapper;
            _dbContext = coreDbContext;
        }

        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(typeof(ApiResponse<string>), 201)]
        public async Task<IActionResult> Create(StoreDto model)
        {
            var store = _mapper.Map<Store>(model);
            store.CreatedBy = Utility.GetUsername();
            store.CreatedDate = DateTime.Now;
            store.WarehouseId = model.WarehouseId;

            await _dbContext.Stores.AddAsync(store);

            var result = await _dbContext.SaveChangesAsync();

            if (result > 0)
                return Created("", ApiResponse<string>.Success());
            else
                return BadRequest(ApiResponse<string>.Error());
        }

        [HttpPut]
        [Route("Update/{id}")]
        [ProducesResponseType(typeof(ApiResponse<string>), 200)]
        public async Task<IActionResult> Update(int id, StoreDto model)
        {
            var existingStore = await _dbContext.Stores.FindAsync(id);

            if (existingStore == null)
                return NotFound(ApiResponse<string>.NotFound());

            existingStore.Name = model.Name;
            existingStore.Description = model.Description;
            existingStore.IsActive = model.IsActive;
            existingStore.ModifiedBy = Utility.GetUsername();
            existingStore.ModifiedDate = DateTime.Now;
            existingStore.WarehouseId = model.WarehouseId;

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
            var result = await _dbContext.Stores.ToListAsync();

            if (result == null)
                return BadRequest(ApiResponse<string>.Error("Error Occurred"));

            if (result.Count() == 0)
                return NotFound(ApiResponse<string>.NotFound());

            var response = ApiResponse<List<Store>>.Success(result);

            return Ok(response);
        }

        [HttpGet]
        [Route("GetById/{id}")]
        [ProducesResponseType(typeof(ApiResponse<Warehouse>), 200)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _dbContext.Stores.FindAsync(id);

            if (result == null)
                return NotFound(ApiResponse<string>.NotFound());

            return Ok(ApiResponse<Store>.Success(result));
        }
    }
}
