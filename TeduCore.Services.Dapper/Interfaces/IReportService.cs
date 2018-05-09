using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TeduCore.Services.Dapper.ViewModels;

namespace TeduCore.Services.Dapper.Interfaces
{
    public interface IReportService
    {
        Task<IEnumerable<ReportViewModel>> GetReportAsync(string fromDate, string toDate);
    }
}
