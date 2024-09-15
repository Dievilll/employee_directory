using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Employee;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeeController: ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepo;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepo = employeeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObjectEmployee query)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var employees = await _employeeRepo.GetAllAsync(query);
            var employeeDto = employees.Select(s => s.ToEmployeeDto());

            return Ok(employeeDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var employees = await _employeeRepo.GetByIdAsync(id);

            if(employees == null)
            {
                return NotFound();
            }

            return Ok(employees.ToEmployeeDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] EmployeeCreateDto employeeDto)
        {
            byte[] photoBytes = null;

            if (employeeDto.Photo != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await employeeDto.Photo.CopyToAsync(memoryStream);
                    photoBytes = memoryStream.ToArray();
                }
            }

            var employee = new Employee
            {
                FullName = employeeDto.FullName,
                PhoneNumber = employeeDto.PhoneNumber,
                Photo = photoBytes,
                DepartmentId = employeeDto.DepartmentId,
                PositionId = employeeDto.PositionId
            };

            var createdEmployee = await _employeeRepo.CreateAsync(employee);
            var createdEmployeeDto = createdEmployee.ToEmployeeDto();

            return CreatedAtAction(nameof(GetById), new { id = createdEmployeeDto.EmployeeId }, createdEmployeeDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] EmployeeUpdateDto employeeDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var employeeModel = await _employeeRepo.GetByIdAsync(id);

            if(employeeModel == null)
            {
                return NotFound();
            }

            await _employeeRepo.UpdateAsync(id, employeeDto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var employeeModel = await _employeeRepo.GetByIdAsync(id);
            
            if(employeeModel == null)
            {
                return NotFound();
            }

            await _employeeRepo.DeleteAsync(id);

            return NoContent();
        }

    }
}