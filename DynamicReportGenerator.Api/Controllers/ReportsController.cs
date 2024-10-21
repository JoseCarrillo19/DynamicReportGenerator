using DynamicReportGenerator.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DynamicReportGenerator.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GenerateReport(int userId)
        {
            var report = await _reportService.GenerateUserReportAsync(userId);
            return report.FileContent != null
                ? File(report.FileContent, report.ContentType, report.FileName)
                : Ok(report);
        }
    }
}
