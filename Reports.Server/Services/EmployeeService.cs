using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Reports.DAL.Entities;
using Reports.DAL.Tools;
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
            List<Employee> employees = await _context.Employees.ToListAsync();
            return employees.Where(item => 
                string.Equals(item.Name, name, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<Employee> FindById(Guid id)
        {
            return await _context.Employees.FindAsync(id);
        }

        public async Task<Employee> AddSubordinate(Guid chiefId, Guid employeeId)
        {
            Employee chief = await _context.Employees.FindAsync(chiefId);
            Employee employee = await _context.Employees.FindAsync(employeeId);
            if (chief is null)
            {
                throw new ReportException("chief with this id doesn't exists");
            }
            if (employee is null)
            {
                throw new ReportException("employee with this id doesn't exists");
            }
            if (employee.GetListOfAllSubordinates().Contains(chief))
            {
                throw new ReportException("employee can't be chief of himself");
            }
            chief.AddSubordinate(employee);
            _context.Employees.Update(chief);
            await _context.SaveChangesAsync();
            return chief;
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            return await _context.Employees.ToListAsync();
        }

        public async void Delete(Guid id)
        {
            Employee employee = await _context.Employees.FindAsync(id);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }

        public Employee Update(Employee entity)
        {
            _context.Employees.Update(entity);
            _context.SaveChanges();
            return entity;
        }
    }
}