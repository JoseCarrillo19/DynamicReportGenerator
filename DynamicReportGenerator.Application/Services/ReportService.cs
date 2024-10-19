using DynamicReportGenerator.Application.Interfaces;
using DynamicReportGenerator.Domain.Entities.Enum;
using DynamicReportGenerator.Domain.Entities;
using DynamicReportGenerator.Domain.Interfaces;
using DynamicReportGenerator.Application.Factories;
using DynamicReportGenerator.Repositories.Singleton;

namespace DynamicReportGenerator.Application.Services
{
    public class ReportService : IReportService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Sale> _saleRepository;

        public ReportService(IRepository<User> userRepository, IRepository<Sale> saleRepository)
        {
            _userRepository = userRepository;
            _saleRepository = saleRepository;
        }

        public async Task<Report> GenerateUserReportAsync(int userId)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(userId);

                if (user == null)
                {
                    throw new ArgumentException("Usuario no encontrado.");
                }

                // Uso del Singleton
                var config = ConfigurationManager.Instance;

                // Uso del Factory Method
                var reportGenerator = ReportGeneratorFactory.CreateReportGenerator(user.Role);

                // Obtener datos de ventas desde la base de datos
                var salesData = await _saleRepository.GetAllAsync();

                // Uso del Builder
                var reportBuilder = new ReportBuilder()
                    .SetTitle($"Informe para {user.Name}")
                    .SetSalesData(salesData);

                if (user.Role == UserRole.Director)
                {
                    reportBuilder.SetExecutiveSummary("Análisis detallado de rendimiento.");
                }

                var reportData = reportBuilder.Build();

                // Generar el informe usando el generador específico
                var finalReport = await reportGenerator.GenerateReportAsync(reportData);

                return finalReport;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
