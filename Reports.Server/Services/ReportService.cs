using System;
using System.Threading.Tasks;
using Reports.DAL.Entities;
using Reports.Server.Database;

namespace Reports.Server.Services
{
    public class ReportService : IReportService
    {
        private readonly ReportsDatabaseContext _context;
        
        public ReportService(ReportsDatabaseContext context)
        {
            _context = context;
        }
        
        public async Task<Report> Create(
            Guid taskId, DateTime resolveDate)
        {
            var task = await _context.Tasks.FindAsync(taskId);
            var report = new Report(Guid.NewGuid(), resolveDate, task);
            await _context.Reports.AddAsync(report);
            await _context.SaveChangesAsync();
            return report;
        }

        public async Task<Report> FindById(Guid id)
        {
            return await _context.Reports.FindAsync(id);
        }
    }
}