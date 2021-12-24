using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Reports.DAL.DTO;
using Reports.DAL.Entities;
using Reports.Server.Services;

namespace Reports.Server.Controllers
{
    [ApiController]
    [Route("/reports")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }
        
        
        [HttpPost]
        [Route("create")]
        public async Task<Report> CreateReport([FromBody] ReportDto reportDto)
        {
            return await _reportService.Create(reportDto.TaskId, reportDto.ResolveDate);
        }
        
        [HttpGet]
        [Route("findId")]
        public async Task<IActionResult> FindById([FromQuery] Guid id)
        {
            var item = await _reportService.FindById(id);
            if (item is null)
            {
                return NotFound();
            }

            return Ok(item);
        }
    }
}