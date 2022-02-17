using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reports.DAL.Entities;

namespace Reports.Server.Services
{
    public interface ITaskService
    {
        Task<TaskModel> Create(DateTime deadline, string name, string description);
        Task<TaskModel> FindById(Guid id);
        Task<TaskModel> UpdateTaskEmployee(Guid taskId, Guid employeeId);
        Task<TaskModel> UpdateTaskChanges(Guid taskId, string content);
        Task<TaskModel> CloseTask(Guid taskId);
        Task<IEnumerable<TaskModel>> FindByAssignedEmployee(Guid employeeId);
        Task<IEnumerable<TaskModel>> FindByEditors(Guid employeeId);
        Task<IEnumerable<TaskModel>> AllTaskOfSubs(Guid employeeId);
        Task<IEnumerable<TaskModel>> GetAll();
        Task<IEnumerable<TaskModel>> GetAllClosed();
        Task<IEnumerable<TaskModel>> GetAllOpened();
    }
}