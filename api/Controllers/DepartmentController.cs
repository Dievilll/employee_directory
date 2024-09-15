using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.Department;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/department")]
    [ApiController]
    public class DepartmentController: ControllerBase
    {
        private readonly IDepartmentRepository _departmentRepo; 

        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepo = departmentRepository;

        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObjectDepartment query)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var deps = await _departmentRepo.GetAllAsync(query);
            var depDto = deps.Select(s => s.ToDepartmentDto());

            return Ok(deps);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
                
            var deps = await _departmentRepo.GetByIdAsync(id);

            if(deps == null)
            {
                return NotFound();
            }

            return Ok(deps.ToDepartmentDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DepartmentCreateDto departmentDto)
        {
            if (!ModelState.IsValid)
                    return BadRequest(ModelState);

            var anyDepartmentsExist = await _departmentRepo.AnyExistAsync();

            if (!anyDepartmentsExist)
            {
                if (departmentDto.ParentDepartmentId.HasValue)
                {
                    return BadRequest("Первый отдел не может иметь родительский отдел.");
                }
            }
            else
            {
                if (!departmentDto.ParentDepartmentId.HasValue)
                {
                    return BadRequest("ParentDepartmentId должен быть указан для всех отделов, кроме первого.");
                }

                var parentDepartmentExists = await _departmentRepo.IsExistsAsync(departmentDto.ParentDepartmentId.Value);

                if (!parentDepartmentExists)
                {
                    return BadRequest("Указанный ParentDepartmentId не существует.");
                }
            }

            Department parentDepartment = null;
            if (departmentDto.ParentDepartmentId.HasValue)
            {
                parentDepartment = await _departmentRepo.GetByIdAsync(departmentDto.ParentDepartmentId.Value);
            }

            var departmentModel = new Department
            {
                Name = departmentDto.Name,
                ParentDepartmentId = departmentDto.ParentDepartmentId,
                ParentDepartment = parentDepartment
            };

            await _departmentRepo.CreateAsync(departmentModel);

            return CreatedAtAction(nameof(GetById), new { id = departmentModel.DepartmentId }, departmentModel.ToDepartmentDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] DepartmentUpdateDto departmentDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var departmentModel = await _departmentRepo.GetByIdAsync(id);

            if(departmentModel == null)
            {
                return NotFound();
            }

            await _departmentRepo.UpdateAsync(id, departmentDto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var departmentModel = await _departmentRepo.GetByIdAsync(id);

            if(departmentModel == null)
            {
                return NotFound();
            }

            await _departmentRepo.DeleteAsync(id);

            return NoContent();
        }
            
        // ЗАКОНЧИТЬ С ОПРЕДЕЛЕНИЕМ МЕТОДОВ ИЗ ИНТЕРФЕЙСА IDEPARTMENTREPOSITORY в DEPARTMENTREPOSITORY.cs

        
        
    }
}