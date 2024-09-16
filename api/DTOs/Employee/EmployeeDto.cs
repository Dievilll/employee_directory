using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Department;

namespace api.DTOs.Employee
{
    public class EmployeeDto
    {
        public int EmployeeId { get; set; }
        public string FullName { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int PositionId { get; set; }
        public string PositionTitle { get; set; }
        public string PhoneNumber { get; set; }
        public byte []? Photo { get; set; }
    }
}