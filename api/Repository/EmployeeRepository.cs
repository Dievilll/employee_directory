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
    public class EmployeeRepository : IEmployeeRepository
    {

        private readonly ApplicationDbContext _context;
        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Employee> CreateAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public Task<Employee> DeleteAsync(int employeeId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Employee>> GetAllAsync(QueryObjectEmployee queryObjectEmployee)
        {
            throw new NotImplementedException();
        }

        public async Task<Employee> GetByIdAsync(int employeeId)
        {
            return await _context.Employees
            .Include(e => e.Department)
            .Include(e => e.Position)
            .FirstOrDefaultAsync(e => e.EmployeeId == employeeId);
        }

        public Task<Employee> UpdateAsync(Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}