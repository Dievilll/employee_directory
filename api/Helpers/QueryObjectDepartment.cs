using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Helpers
{
    public class QueryObjectDepartment
    {
        private string _name;
        public string? Name { get => _name; set => _name = Uri.UnescapeDataString(value); }
        public string? PositionTitle { get; set; } = null;
        public string? SortBy { get; set; } = null;
        public bool IsDescending { get; set; } = false;
    }
}