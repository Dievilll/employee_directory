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
using api.DTOs.Department;

namespace api.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _context;

        public DepartmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Department>> GetAllAsync(QueryObjectDepartment query)
        {
            var departments = _context.Departments
            .Include(pd => pd.ParentDepartment)
            .Include(sd => sd.SubDepartments)
            .Include(p => p.Positions)
                .ThenInclude(e => e.Employees)
            .AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Name))
            {
                departments = departments.Where(x => x.Name.ToLower().Contains(query.Name.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(query.PositionTitle))
            {
                departments = departments.Where(x => x.Positions.Any(p => p.Title.ToLower().Contains(query.PositionTitle.ToLower())));
            }

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("PositionTitle", StringComparison.OrdinalIgnoreCase))
                {
                    departments = query.IsDescending ? departments.OrderByDescending(x => x.Positions.Select(p => p.Title)) : departments.OrderBy(x => x.Positions.Select(p => p.Title));
                }
            }

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
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

        public async Task<Department> CreateAsync(DepartmentCreateDto departmentCreateDto)
        {
            var departmentModel = new Department
            {
                Name = departmentCreateDto.Name,
                ParentDepartmentId = departmentCreateDto.ParentDepartmentId,
                Positions = departmentCreateDto.Positions?.Select(p => new Position
                {
                    Title = p.Title,
                    Salary = p.Salary
                }).ToList() ?? new List<Position>()
            };

            await _context.AddAsync(departmentModel);
            await _context.SaveChangesAsync();
            return departmentModel;
        }

        public async Task<Department?> UpdateAsync(int id, DepartmentUpdateDto departmentUpdateDto)
        {
                var departmentModel = await _context.Departments
                .Include(d => d.Positions)
                    .ThenInclude(p => p.Employees)
                .FirstOrDefaultAsync(d => d.DepartmentId == id);

                if (departmentModel == null)
                {
                    throw new ArgumentException("Department not found.");
                }

                departmentModel.Name = departmentUpdateDto.Name;
                departmentModel.ParentDepartmentId = departmentUpdateDto.ParentDepartmentId;

                if (departmentUpdateDto.PositionsToUpdate != null)
                {
                    foreach (var positionUpdate in departmentUpdateDto.PositionsToUpdate)
                    {
                        var position = departmentModel.Positions.FirstOrDefault(p => p.PositionId == positionUpdate.PositionId);
                        if (position != null)
                        {
                            position.Title = positionUpdate.Title;
                            position.Salary = positionUpdate.Salary;
                        }
                    }
                }

                if (departmentUpdateDto.PositionsToCreate != null)
                {
                    foreach (var positionCreate in departmentUpdateDto.PositionsToCreate)
                    {
                        var newPosition = new Position
                        {
                            Title = positionCreate.Title,
                            Salary = positionCreate.Salary
                        };
                        departmentModel.Positions.Add(newPosition);
                    }
                }

                if (departmentUpdateDto.PositionsToDelete != null)
                {
                    foreach (var positionId in departmentUpdateDto.PositionsToDelete)
                    {
                        var position = departmentModel.Positions.FirstOrDefault(p => p.PositionId == positionId);
                        if (position != null)
                        {
                            _context.Employees.RemoveRange(position.Employees);

                            departmentModel.Positions.Remove(position);
                        }
                    }
                }

                await _context.SaveChangesAsync();

            return departmentModel;
        }

        public async Task<Department?> DeleteAsync(int departmentId)
        {
            var departmentModel = await _context.Departments
            .Include(d => d.SubDepartments)
                .ThenInclude(sd => sd.Positions)
                    .ThenInclude(p => p.Employees)
            .Include(d => d.Positions)
                .ThenInclude(p => p.Employees)
            .FirstOrDefaultAsync(d => d.DepartmentId == departmentId);

            if (departmentModel == null)
            {
                throw new ArgumentException("Department not found.");
            }

            foreach (var subDepartment in departmentModel.SubDepartments.ToList())
            {
                await DeleteAsync(subDepartment.DepartmentId);
            }

            foreach (var position in departmentModel.Positions)
            {
                _context.Employees.RemoveRange(position.Employees);
            }

            _context.Positions.RemoveRange(departmentModel.Positions);

            _context.Departments.Remove(departmentModel);

            await _context.SaveChangesAsync();

            return departmentModel;
        }

        public async Task<bool> IsExistsAsync(int departmentId)
        {
            return await _context.Departments.AnyAsync(d => d.DepartmentId == departmentId);
        }

        public async Task<bool> AnyExistAsync()
        {
            return await _context.Departments.AnyAsync();
        }
    }
}