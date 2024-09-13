using System.Runtime.Intrinsics.X86;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Helpers;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _context;

        public DepartmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> IsExistsAsync(int departmentId)
        {
            return await _context.Departments.AnyAsync(d => d.DepartmentId == departmentId);
        }

        public async Task<bool> AnyExistAsync()
        {
            return await _context.Departments.AnyAsync();
        }

        public async Task<Department> CreateAsync(Department departmentModel)
        {
            await _context.AddAsync(departmentModel);
            await _context.SaveChangesAsync();
            return departmentModel;
        }

        public async Task<Department?> DeleteAsync(int departmentId)
        {
            var departmentModel = await _context.Departments.FirstOrDefaultAsync(x => x.DepartmentId == departmentId);
            
            if(departmentModel == null)
            {
                return null;
            }
            _context.Departments.Remove(departmentModel);
            await _context.SaveChangesAsync();

            return departmentModel;
        }

        public async Task<List<Department>> GetAllAsync(QueryObjectDepartment query)
        {
            var departments = _context.Departments
            .Include(pd => pd.ParentDepartment)
            .Include(sd => sd.SubDepartments)
            .Include(p => p.Positions)
            .AsQueryable();

            if(!string.IsNullOrWhiteSpace(query.Name))
            {
                departments = departments.Where(x => x.Name.Contains(query.Name));
            }

            if(!string.IsNullOrWhiteSpace(query.PositionTitle))
            {
                departments = departments.Where(x => x.Positions.Any(p => p.Title.Contains(query.PositionTitle)));
            }

            if(!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if(query.SortBy.Equals("PositionTitle", StringComparison.OrdinalIgnoreCase))
                {
                    departments = query.IsDescending ? departments.OrderByDescending(x => x.Positions.Select(p => p.Title)) : departments.OrderBy(x => x.Positions.Select(p => p.Title));
                }
            }

            if(!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if(query.SortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    departments = query.IsDescending ? departments.OrderByDescending(x => x.Name) : departments.OrderBy(x => x.Name);
                }
            }

            return await departments.ToListAsync();
        }

        public async Task<Department?> GetByIdAsync(int departmentId)
        {
            return await _context.Departments
            .Include(pd => pd.ParentDepartment)
            .Include(sd => sd.SubDepartments)
            .Include(p => p.Positions)
                .ThenInclude(e => e.Employees)
            .FirstOrDefaultAsync(d => d.DepartmentId == departmentId);
        }

        public async Task<Department?> UpdateAsync(int id, Department department)
        {
            var departmentModel = await _context.Departments.FirstOrDefaultAsync(x => x.DepartmentId == id);

            if(departmentModel == null)
            {
                return null;
            }

            departmentModel.Name = department.Name;
            departmentModel.ParentDepartmentId = department.ParentDepartmentId;

            await _context.SaveChangesAsync();

            return departmentModel;
        }
    }
}