using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.Employee;
using api.Helpers;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {

        private readonly ApplicationDbContext _context;
        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Employee>> GetAllAsync(QueryObjectEmployee queryObjectEmployee)
        {
            var employees = _context.Employees
            .Include(d => d.Department)
            .Include(p => p.Position)
            .AsQueryable();

            if(!string.IsNullOrWhiteSpace(queryObjectEmployee.FullName))
            {
                employees = employees.Where(x => x.FullName.ToLower().Contains(queryObjectEmployee.FullName.ToLower()));
            }

            if(!string.IsNullOrWhiteSpace(queryObjectEmployee.PhoneNumber))
            {
                employees = employees.Where(x => x.PhoneNumber.Contains(queryObjectEmployee.PhoneNumber));
            }

            if(!string.IsNullOrWhiteSpace(queryObjectEmployee.DepartmentName))
            {
                
                employees = employees.Include(d => d.Department)
                .Where(x => x.Department.Name.ToLower()
                .Contains(queryObjectEmployee.DepartmentName.ToLower()));
            }

            if(!string.IsNullOrWhiteSpace(queryObjectEmployee.SortBy))
            {
                if(queryObjectEmployee.SortBy.Equals("FullName", StringComparison.OrdinalIgnoreCase))
                {
                    employees = queryObjectEmployee.IsDescending ? employees.OrderByDescending(x => x.FullName) : employees.OrderBy(x => x.FullName);
                }

                if(queryObjectEmployee.SortBy.Equals("PhoneNumber", StringComparison.OrdinalIgnoreCase))
                {
                    employees = queryObjectEmployee.IsDescending ? employees.OrderByDescending(x => x.PhoneNumber) : employees.OrderBy(x => x.PhoneNumber);
                }

                if(queryObjectEmployee.SortBy.Equals("DepartmentName", StringComparison.OrdinalIgnoreCase))
                {
                    employees = queryObjectEmployee.IsDescending ? employees.OrderByDescending(x => x.Department.Name) : employees.OrderBy(x => x.Department.Name);
                }
            }

            return await employees.ToListAsync();
        }

        public async Task<Employee?> GetByIdAsync(int employeeId)
        {
            return await _context.Employees
            .Include(e => e.Department)
            .Include(e => e.Position)
            .FirstOrDefaultAsync(e => e.EmployeeId == employeeId);
        }

        public async Task<Employee> CreateAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee?> UpdateAsync(int id, EmployeeUpdateDto employeeUpdate)
        {
            var employeeModel = await _context.Employees.FirstOrDefaultAsync(x => x.EmployeeId == id);

            if(employeeModel == null)
            {
                return null;
            }

            employeeModel.FullName = employeeUpdate.FullName;
            employeeModel.DepartmentId = employeeUpdate.DepartmentId;
            employeeModel.PositionId = employeeUpdate.PositionId;
            employeeModel.PhoneNumber = employeeUpdate.PhoneNumber;
            if (employeeUpdate.Photo != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await employeeUpdate.Photo.CopyToAsync(memoryStream);
                    employeeModel.Photo = memoryStream.ToArray();
                }
            }

            await _context.SaveChangesAsync();

            return employeeModel;
        }

        public async Task<Employee?> DeleteAsync(int employeeId)
        {
            var employeeModel = await _context.Employees.FirstOrDefaultAsync(x => x.EmployeeId == employeeId);
            
            if(employeeModel == null)
            {
                return null;
            }

            _context.Employees.Remove(employeeModel);
            await _context.SaveChangesAsync();

            return employeeModel;
        }

        public async Task<bool> IsExistsAsync(int employeeId)
        {
            return await _context.Employees.AnyAsync(e => e.EmployeeId == employeeId);
        }

        public async Task<bool> AnyExistAsync()
        {
            return await _context.Employees.AnyAsync();
        }
    }
}