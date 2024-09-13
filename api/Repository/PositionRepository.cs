using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
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

        public Task<bool> AnyExistAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Position> CreateAsync(Position position)
        {
            await _context.Positions.AddAsync(position);
            await _context.SaveChangesAsync();

            return position;
        }

        public Task<Position> DeleteAsync(int positionId)
        {
            throw new NotImplementedException();
        }

        public async Task<Position> GetByIdAsync(int positionId)
        {
            return await _context.Positions.Include(e => e.Employees).FirstOrDefaultAsync(pi => pi.PositionId == positionId);
        }

        public Task<bool> IsExistsAsync(int positionId)
        {
            throw new NotImplementedException();
        }

        public Task<Position> UpdateAsync(Position position)
        {
            throw new NotImplementedException();
        }
    }
}