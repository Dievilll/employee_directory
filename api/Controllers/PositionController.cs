using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Position;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/position")]
    [ApiController]
    public class PositionController: ControllerBase
    {
        private readonly IPositionRepository _positionRepo;

        public PositionController(IPositionRepository positionRepository)
        {
            _positionRepo = positionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var positions = await _positionRepo.GetAllAsync();
            var positionDto = positions.Select(s => s.ToPositionDto());

            return Ok(positionDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var position = await _positionRepo.GetByIdAsync(id);
            
            if(position == null)
            {
                return NotFound();
            }

            return Ok(position.ToPositionDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PositionCreateDto positionDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var position = positionDto.ToPositionCreateDto();
            await _positionRepo.CreateAsync(position);

            return CreatedAtAction(nameof(GetById), new {id = position.PositionId}, position.ToPositionDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PositionUpdateDto positionDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var positionModel = await _positionRepo.GetByIdAsync(id);

            if(positionModel == null)
            {
                return NotFound();
            }

            await _positionRepo.UpdateAsync(id, positionDto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var positionModel = await _positionRepo.GetByIdAsync(id);
            
            if(positionModel == null)
            {
                return NotFound();
            }

            await _positionRepo.DeleteAsync(id);

            return NoContent();
        }

        [HttpGet("{departmentId}/positions")]
        public async Task<IActionResult> GetPositionsByDepartmentId(int departmentId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var positions = await _positionRepo.GetPositionsByDepartmentIdAsync(departmentId);

            if (positions == null || positions.Count == 0)
            {
                return NotFound();
            }

            return Ok(positions);
        }
    }
}