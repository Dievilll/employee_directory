using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Position;
using api.Models;

namespace api.Interfaces
{
    public interface IPositionRepository
    {
        Task<List<Position>> GetAllAsync();
        Task<Position?> GetByIdAsync(int positionId);
        Task<Position> CreateAsync(Position positionModel);
        Task<Position?> UpdateAsync(int id, PositionUpdateDto positionUpdate);
        Task<Position?> DeleteAsync(int positionId);
        Task<bool> IsExistsAsync(int positionId);

        Task<bool> AnyExistAsync();
    }
}