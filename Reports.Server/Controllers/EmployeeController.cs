using System;
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

        [HttpPost]
        [Route("create")]
        public async Task<Employee> Create([FromBody] EmployeeDto dto)
        {
            return await _service.Create(dto.Name); 
        }

        [HttpGet]
        [Route("findName")]
        public async Task<IActionResult> FindByName([FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return StatusCode((int)HttpStatusCode.BadRequest);
            var result = await _service.FindByName(name);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        [HttpGet]
        [Route("findId")]
        public async Task<IActionResult> FindById([FromQuery] Guid id)
        {
            if (id == Guid.Empty) return StatusCode((int)HttpStatusCode.BadRequest);
            var result = await _service.FindById(id);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }
        
        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAll();
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }
        
        [HttpPatch]
        [Route("AddSubordinate")]
        public async Task<IActionResult> SetNewChief(
            [FromQuery] Guid chiefId,
            [FromQuery] Guid employeeId
            )
        {
            var chief = await _service.FindById(chiefId);
            if (chief is null)
            {
                return NotFound();
            }
            var employee = await _service.FindById(employeeId);
            if (employee is null)
            {
                return NotFound();
            }
            chief.AddSubordinate(employee);
            return Ok(chief);
        }
    }
}