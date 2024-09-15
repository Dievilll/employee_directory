using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Helpers
{
    public class QueryObjectEmployee
    {
        public string? FullName { get; set; } = null;

        public string? PhoneNumber { get; set; } = null;

        public string? DepartmentName { get; set; } = string.Empty;

        public string? SortBy { get; set; } = string.Empty;
        public bool IsDescending { get; set; } = false;
    }
}