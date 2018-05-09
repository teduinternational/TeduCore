using System;
using System.Collections.Generic;
using System.Text;

namespace TeduCore.Services.Dapper.ViewModels
{
    public class ReportViewModel
    {
        public DateTime Date { get; set; }
        public decimal Revenue { get; set; }

        public decimal Profit { get; set; }
    }
}
