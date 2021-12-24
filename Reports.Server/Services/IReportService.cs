using System;
using System.Threading.Tasks;
using Reports.DAL.Entities;

namespace Reports.Server.Services
{
    public interface IReportService
    {
        public Task<Report> Create(Guid taskId, DateTime resolveDate);
        public Task<Report> FindById(Guid id);
    }
}