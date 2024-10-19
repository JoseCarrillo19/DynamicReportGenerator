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
            try
            {
                var report = await _reportService.GenerateUserReportAsync(userId);

                if (report.FileContent != null)
                {
                    return File(report.FileContent, report.ContentType, report.FileName);
                }
                else
                {
                    return Ok(report);
                }
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al generar el informe: {ex.Message}");
            }
        }
    }
}
