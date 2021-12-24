using System;
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

        [HttpPost]
        [Route("create")]
        public async Task<TaskModel> CreateTask([FromBody] TaskDto taskModel)
        {
            return await _taskService.Create(
                taskModel.Deadline,
                taskModel.Name,
                taskModel.Description);
        }

        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _taskService.GetAll();
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        [HttpGet]
        [Route("findId")]
        public async Task<IActionResult> FindById([FromQuery]Guid id)
        {
            var result = await _taskService.FindById(id);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }
        
        [HttpPatch]
        [Route("updateTask")]
        public async Task<IActionResult> AddTaskChanges([FromBody] TaskChangesDto changes)
        {
            var task = await _taskService.FindById(changes.TaskId);
            if (task is null)
            {
                return NotFound();
            }

            await _taskService.UpdateTaskChanges(changes.TaskId, changes.Content);
            return Ok(task);
        }
        
        [HttpPatch]
        [Route("close")]
        public async Task<IActionResult> CloseTask([FromQuery] Guid taskId)
        {
            var task = await _taskService.CloseTask(taskId);
            return Ok(task);
        }
        
        [HttpPut]
        [Route("updateEmployee")]
        public async Task<IActionResult> SetNewEmployee([FromBody] UpdateEmployeeDto employeeDto)
        {
            var task = await _taskService.FindById(employeeDto.TaskId);
            if (task is null)
            {
                return NotFound();
            }
            await _taskService.UpdateTaskEmployee(employeeDto.TaskId, employeeDto.EmployeeId);
            return Ok(task);
        }

    }
}