using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Reports.DAL.Entities;
using Reports.Server.Controllers;
using Reports.Server.Database;

namespace Reports.Server.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ReportsDatabaseContext _context;

        public EmployeeService(ReportsDatabaseContext context)
        {
            _context = context;
        }

        public async Task<Employee> Create(string name)
        {
            var employee = new Employee(Guid.NewGuid(), name);
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<IEnumerable<Employee>> FindByName(string name)
        {
            var employees = await _context.Employees.ToListAsync();
            return employees.Where(item => 
                string.Equals(item.Name, name, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<Employee> FindById(Guid id)
        {
            return await _context.Employees.FindAsync(id);
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            return await _context.Employees.ToListAsync();
        }

        public async void Delete(Guid id)
        {
            var employee = await _context.Employees.FindAsync(id);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }

        public Employee Update(Employee entity)
        {
            throw new NotImplementedException();
        }
    }
}