using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Position;

namespace api.DTOs.Department
{
    public class DepartmentCreateDto
    {
        [Required]
        [MaxLength(100, ErrorMessage ="Название отдела не должно превышать длины в 100 символов.")]
        public string Name { get; set; }

        public int? ParentDepartmentId { get; set; }

        public List<PositionCreateDto>? Positions { get; set; }
    }
}