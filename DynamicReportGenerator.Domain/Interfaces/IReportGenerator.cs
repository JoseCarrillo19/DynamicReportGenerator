using DynamicReportGenerator.Domain.Entities;

namespace DynamicReportGenerator.Domain.Interfaces
{
    public interface IReportGenerator
    {
        Task<Report> GenerateReportAsync(Report reportData);
    }
}
