using DynamicReportGenerator.Domain.Entities;

namespace DynamicReportGenerator.Application.Services
{
    public class ReportBuilder
    {
        private readonly Report _report;

        public ReportBuilder()
        {
            _report = new Report();
        }

        public ReportBuilder SetTitle(string title)
        {
            _report.Title = title;
            return this;
        }

        public ReportBuilder SetSalesData(IEnumerable<Sale> salesData)
        {
            _report.SalesData = salesData;
            return this;
        }

        public ReportBuilder SetExecutiveSummary(string summary)
        {
            _report.ExecutiveSummary = summary;
            return this;
        }

        public Report Build()
        {
            return _report;
        }
    }
}
