using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface IPositionRepository
    {
        Task<Position> GetByIdAsync(int positionId);
        Task<Position> CreateAsync(Position position);
        Task<Position> UpdateAsync(Position position);
        Task<Position> DeleteAsync(int positionId);
        Task<bool> IsExistsAsync(int positionId);

        Task<bool> AnyExistAsync();
    }
}