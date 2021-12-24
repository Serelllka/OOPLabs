using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reports.DAL.Entities;

namespace Reports.Server.Services
{
    public interface IEmployeeService
    {
        Task<Employee> Create(string name);

        Task<IEnumerable<Employee>> FindByName(string name);

        Task<Employee> FindById(Guid id);

        Task<IEnumerable<Employee>> GetAll();

        void Delete(Guid id);

        Employee Update(Employee entity);
    }
}