using DynamicReportGenerator.Domain.Entities;
using DynamicReportGenerator.Domain.Interfaces;
using OfficeOpenXml;
using LicenseContext = OfficeOpenXml.LicenseContext;

namespace DynamicReportGenerator.Application.Reports
{
    public class AnalystReportGenerator : IReportGenerator
    {
        public async Task<Report> GenerateReportAsync(Report reportData)
        {
            // Lógica para generar el informe de analista
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using var package = new ExcelPackage();

            var worksheet = package.Workbook.Worksheets.Add("Informe Analista");

            worksheet.Cells[1, 1].Value = reportData.Title;

            // Escribir encabezados
            worksheet.Cells[3, 1].Value = "Región";
            worksheet.Cells[3, 2].Value = "Monto";
            worksheet.Cells[3, 3].Value = "Fecha";

            int row = 4;
            foreach (var sale in reportData.SalesData.OrderBy(s => s.Date))
            {
                worksheet.Cells[row, 1].Value = sale.Region;
                worksheet.Cells[row, 2].Value = sale.Amount;
                worksheet.Cells[row, 3].Value = sale.Date.ToShortDateString();
                row++;
            }

            // Formatear celdas
            worksheet.Column(2).Style.Numberformat.Format = "#,##0.00";

            // Ajustar ancho de columnas
            worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

            // Guardar el archivo en memoria
            var fileBytes = package.GetAsByteArray();

            reportData.FileContent = fileBytes;
            reportData.FileName = "AnalystReport.xlsx";
            reportData.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            await Task.CompletedTask;
            return reportData;
        }
    }
}
