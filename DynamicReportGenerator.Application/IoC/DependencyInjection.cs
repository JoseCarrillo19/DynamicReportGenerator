using DynamicReportGenerator.Application.Interfaces;
using DynamicReportGenerator.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DynamicReportGenerator.Application.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IReportService, ReportService>();
            return services;
        }
    }
}
