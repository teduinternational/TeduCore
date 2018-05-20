using System.Collections.Generic;
using System.Threading.Tasks;
using TeduCore.Application.Dapper.Reports.Dtos;

namespace TeduCore.Application.Dapper.Reports
{
    public interface IReportService
    {
        Task<IEnumerable<ReportViewModel>> GetReportAsync(string fromDate, string toDate);
    }
}