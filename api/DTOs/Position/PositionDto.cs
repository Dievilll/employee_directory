using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Employee;

namespace api.DTOs.Position
{
    public class PositionDto
    {
        public int PositionId { get; set; }
        public string Title { get; set; }
        public decimal Salary { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public List<EmployeeDto> Employees { get; set; }
    }
}