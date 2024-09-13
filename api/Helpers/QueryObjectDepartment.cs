using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Helpers
{
    public class QueryObjectDepartment
    {
    public string? Name { get; set; } = null;
    public string? PositionTitle { get; set; } = null;
    public string? SortBy { get; set; } = null;
    public bool IsDescending { get; set; } = false;
    }
}