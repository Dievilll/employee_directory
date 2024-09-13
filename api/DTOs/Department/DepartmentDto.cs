using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Position;

namespace api.DTOs.Department
{
    public class DepartmentDto
    {
        public int DepartmentId { get; set; }

        public string Name { get; set; }

        public int? ParentDepartmentId { get; set; }

        public string ParentDepartmentName { get; set; }

        public List<DepartmentDto> SubDepartments { get; set; }

        public List<PositionDto> Positions { get; set; }
    }

    
}