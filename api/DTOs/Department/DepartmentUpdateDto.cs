using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Position;

namespace api.DTOs.Department
{
    public class DepartmentUpdateDto
    {
        public string Name { get; set; }
        public int? ParentDepartmentId { get; set; }
        public List<PositionUpdateDto>? PositionsToUpdate { get; set; }
        public List<PositionCreateDto>? PositionsToCreate { get; set; }
        public List<int>? PositionsToDelete { get; set; }
    }
}