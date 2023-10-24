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
    public class SupplierController : ControllerBase
    {
        private readonly CoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public SupplierController(CoreDbContext coreDbContext, IMapper mapper)
        {
            _mapper = mapper;
            _dbContext = coreDbContext;
        }

        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(typeof(ApiResponse<string>), 201)]
        public async Task<IActionResult> Create(SupplierDto model)
        {
            var supplier = _mapper.Map<Supplier>(model);
            supplier.CreatedBy = Utility.GetUsername();
            supplier.CreatedDate = DateTime.Now;

            await _dbContext.Suppliers.AddAsync(supplier);

            var result = await _dbContext.SaveChangesAsync();

            if (result > 0)
                return Created("", ApiResponse<string>.Success());
            else
                return BadRequest(ApiResponse<string>.Error());
        }

        [HttpPut]
        [Route("Update/{id}")]
        [ProducesResponseType(typeof(ApiResponse<string>), 200)]
        public async Task<IActionResult> Update(int id, SupplierDto model)
        {
            var existingSupplier = await _dbContext.Suppliers.FindAsync(id);

            if (existingSupplier == null)
                return NotFound(ApiResponse<string>.NotFound());

            existingSupplier.Name = model.Name;
            existingSupplier.ContactInformation = model.ContactInformation;
            existingSupplier.Description = model.Description;
            existingSupplier.IsActive = model.IsActive;
            existingSupplier.ModifiedBy = Utility.GetUsername();
            existingSupplier.ModifiedDate = DateTime.Now;

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
            var result = await _dbContext.Suppliers.ToListAsync();

            if (result == null)
                return BadRequest(ApiResponse<string>.Error("Error Occurred"));

            if (result.Count() == 0)
                return NotFound(ApiResponse<string>.NotFound());

            var response = ApiResponse<List<Supplier>>.Success(result);

            return Ok(response);
        }

        [HttpGet]
        [Route("GetById/{id}")]
        [ProducesResponseType(typeof(ApiResponse<Supplier>), 200)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _dbContext.Suppliers.FindAsync(id);

            if (result == null)
                return NotFound(ApiResponse<string>.NotFound());

            return Ok(ApiResponse<Supplier>.Success(result));
        }
    }
}