using DynamicReportGenerator.Domain.Entities;

namespace DynamicReportGenerator.Application.Interfaces
{
    public interface IReportService
    {
        Task<Report> GenerateUserReportAsync(int userId);
    }
}
