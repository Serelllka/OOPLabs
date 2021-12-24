using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Reports.DAL.Entities;
using Reports.DAL.Tools;
using Reports.Server.Database;

namespace Reports.Server.Services
{
    public class TaskService : ITaskService
    {
        private readonly ReportsDatabaseContext _context;

        public TaskService(ReportsDatabaseContext context)
        {
            _context = context;
        }
        
        public async Task<TaskModel> Create(
            DateTime deadline,
            string name,
            string description)
        {
            var task = new TaskModel(deadline, name, description);
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<TaskModel> FindById(Guid id)
        {
            return await _context.Tasks.FindAsync(id);
        }

        public async Task<TaskModel> UpdateTaskEmployee(Guid taskId, Guid employeeId)
        {
            var task = await _context.Tasks.FindAsync(taskId);
            var employee = await _context.Employees.FindAsync(employeeId);
            task.UpdateAssignedEmployee(employee);
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<TaskModel> UpdateTaskChanges(Guid taskId, string content)
        {
            var task = await _context.Tasks.FindAsync(taskId);
            var entry = new Entry(Guid.NewGuid(), task.AssignedEmployee, content);
            if (task.State == TaskState.Closed)
            {
                return task;
            }
            task.AddChanges(entry);
            _context.Entries.Add(entry);
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<TaskModel> CloseTask(Guid taskId)
        {
            var task = await FindById(taskId);
            task.CloseTask();
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<IEnumerable<TaskModel>> GetAll()
        {
            return await _context.Tasks.ToListAsync();
        }
    }
}