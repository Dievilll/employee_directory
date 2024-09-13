using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public int? ParentDepartmentId { get; set; }

        [ForeignKey("ParentDepartmentId")]
        public Department ParentDepartment { get; set; }

        public List<Department> SubDepartments { get; set; }

        public List<Position> Positions { get; set; }
    }

    
}