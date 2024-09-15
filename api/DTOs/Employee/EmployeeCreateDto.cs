using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.Employee
{
    public class EmployeeCreateDto
    {
            public string FullName { get; set; }
            public int DepartmentId { get; set; }
            public int PositionId { get; set; }
            public string PhoneNumber { get; set; }
            public IFormFile Photo { get; set; }
    }
}