using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.Position
{
    public class PositionCreateDto
    {
        public string Title { get; set; }
        public decimal Salary { get; set; }
        public int DepartmentId { get; set; }
    }
}