using DynamicReportGenerator.Application.Reports;
using DynamicReportGenerator.Domain.Entities.Enum;
using DynamicReportGenerator.Domain.Interfaces;

namespace DynamicReportGenerator.Application.Factories
{
    public class ReportGeneratorFactory
    {
        public static IReportGenerator CreateReportGenerator(UserRole role)
        {
            return role switch
            {
                UserRole.Analyst => new AnalystReportGenerator(),
                UserRole.Manager => new ManagerReportGenerator(),
                UserRole.Director => new DirectorReportGenerator(),
                _ => throw new ArgumentException("Rol de usuario no válido"),
            };
        }
    }
}
