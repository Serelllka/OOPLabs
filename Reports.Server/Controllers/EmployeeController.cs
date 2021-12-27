using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Reports.DAL.DTO;
using Reports.DAL.Entities;
using Reports.Server.Services;

namespace Reports.Server.Controllers
{
    [ApiController]
    [Route("/employees")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _service;

        public EmployeeController(IEmployeeService service)
        {
            _service = service;
        }

        [HttpPost("create")]
        public async Task<Employee> Create([FromBody] EmployeeDto dto)
        {
            return await _service.Create(dto.Name); 
        }

        [HttpGet("findName")]
        public async Task<IActionResult> FindByName([FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return StatusCode((int)HttpStatusCode.BadRequest);
            IEnumerable<Employee> result = await _service.FindByName(name);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        [HttpGet("findId")]
        public async Task<IActionResult> FindById([FromQuery] Guid id)
        {
            if (id == Guid.Empty) return StatusCode((int)HttpStatusCode.BadRequest);
            Employee result = await _service.FindById(id);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }
        
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<Employee> result = await _service.GetAll();
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }
        
        [HttpPatch("AddSubordinate")]
        public async Task<IActionResult> SetNewChief(
            [FromQuery] Guid chiefId,
            [FromQuery] Guid employeeId
            )
        {
            Employee chief = await _service.FindById(chiefId);
            Employee employee = await _service.FindById(employeeId);
            if (chief is null || employee is null)
            {
                return NotFound();
            }

            await _service.AddSubordinate(chiefId, employeeId);
            return Ok(chief);
        }
    }
}