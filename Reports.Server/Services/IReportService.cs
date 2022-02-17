using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reports.DAL.Entities;

namespace Reports.Server.Services
{
    public interface IReportService
    {
        Task<Report> Create(Guid taskId, DateTime resolveDate);
        Task<Report> FindById(Guid id);
        Task<IEnumerable<Report>> GetAll();
    }
}