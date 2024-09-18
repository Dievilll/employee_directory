using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.Position;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    
    public class PositionRepository : IPositionRepository
    {
        private readonly ApplicationDbContext _context;
        public PositionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Position>> GetAllAsync()
        {
            var positions = _context.Positions.Include(d => d.Department).Include(e => e.Employees);
            return await positions.ToListAsync();
        }

        public async Task<Position?> GetByIdAsync(int positionId)
        {
            return await _context.Positions
            .Include(d => d.Department)
            .Include(e => e.Employees)
            .FirstOrDefaultAsync(pi => pi.PositionId == positionId);
        }

        public async Task<Position> CreateAsync(Position position)
        {
            await _context.Positions.AddAsync(position);
            await _context.SaveChangesAsync();

            return position;
        }

        public async Task<Position?> UpdateAsync(int id, PositionUpdateDto positionUpdate)
        {
            var posModel = await _context.Positions.FirstOrDefaultAsync(x => x.PositionId == id);

            if(posModel == null)
            {
                return null;
            }

            posModel.Title = positionUpdate.Title;
            posModel.Salary = positionUpdate.Salary;
            posModel.DepartmentId = positionUpdate.DepartmentId;

            await _context.SaveChangesAsync();
            
            return posModel;
        }

        public async Task<Position?> DeleteAsync(int positionId)
        {
            var posModel = await _context.Positions
                .Include(p => p.Employees)
                .FirstOrDefaultAsync(p => p.PositionId == positionId);

            if (posModel == null)
            {
                throw new ArgumentException("Position not found.");
            }

            _context.Employees.RemoveRange(posModel.Employees);

            _context.Positions.Remove(posModel);

            await _context.SaveChangesAsync();

            return posModel;
        }

        public async Task<List<PositionDto>> GetPositionsByDepartmentIdAsync(int departmentId)
        {
            return await _context.Positions
                .Where(p => p.DepartmentId == departmentId)
                .Select(p => new PositionDto
                {
                    PositionId = p.PositionId,
                    Title = p.Title
                })
                .ToListAsync();
        }

        public async Task<bool> IsExistsAsync(int positionId)
        {
            return await _context.Positions.AnyAsync(pi => pi.PositionId == positionId);
        }

        public async Task<bool> AnyExistAsync()
        {
            return await _context.Positions.AnyAsync();
        }
    }
}