using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reports.DAL.DTO;
using Reports.DAL.Entities;
using Reports.Server.Database;
using Reports.Server.Services;

namespace Reports.Server.Controllers
{
    [ApiController]
    [Route("/tasks")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }
        
        [HttpPost("create")]
        public async Task<TaskModel> CreateTask([FromBody] TaskDto taskModel)
        {
            return await _taskService.Create(
                taskModel.Deadline,
                taskModel.Name,
                taskModel.Description);
        }
        
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<TaskModel> result = await _taskService.GetAll();
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }
        
        [HttpGet("getAllClosed")]
        public async Task<IActionResult> GetAllClosed()
        {
            IEnumerable<TaskModel> result = await _taskService.GetAllClosed();
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }
        
        [HttpGet("getAllOpened")]
        public async Task<IActionResult> GetAllOpened()
        {
            IEnumerable<TaskModel> result = await _taskService.GetAllOpened();
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }
        
        [HttpGet("findId")]
        public async Task<IActionResult> FindById([FromQuery]Guid id)
        {
            TaskModel result = await _taskService.FindById(id);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }
        
        [HttpGet("findAssignedEmployee")]
        public async Task<IActionResult> FindByAssignedEmployee
            ([FromQuery]Guid employeeId)
        {
            IEnumerable<TaskModel> result = await _taskService.FindByAssignedEmployee(employeeId);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }
        
        [HttpGet("findEditors")]
        public async Task<IActionResult> FindByEditors
            ([FromQuery]Guid employeeId)
        {
            IEnumerable<TaskModel> result = await _taskService.FindByEditors(employeeId);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }
        
        [HttpGet("taskOfSubs")]
        public async Task<IActionResult> GetAllTaskOfSubs([FromQuery]Guid employeeId)
        {
            IEnumerable<TaskModel> result = await _taskService.FindByAssignedEmployee(employeeId);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }
        
        [HttpPatch("updateTask")]
        public async Task<IActionResult> AddTaskChanges([FromBody] TaskChangesDto changes)
        {
            TaskModel task = await _taskService.FindById(changes.TaskId);
            if (task is null)
            {
                return NotFound();
            }

            await _taskService.UpdateTaskChanges(changes.TaskId, changes.Content);
            return Ok(task);
        }
        
        [HttpPatch("close")]
        public async Task<IActionResult> CloseTask([FromQuery] Guid taskId)
        {
            TaskModel task = await _taskService.CloseTask(taskId);
            return Ok(task);
        }
        
        [HttpPut("updateEmployee")]
        public async Task<IActionResult> SetNewEmployee([FromBody] UpdateEmployeeDto employeeDto)
        {
            TaskModel task = await _taskService.FindById(employeeDto.TaskId);
            if (task is null)
            {
                return NotFound();
            }
            await _taskService.UpdateTaskEmployee(employeeDto.TaskId, employeeDto.EmployeeId);
            return Ok(task);
        }

    }
}