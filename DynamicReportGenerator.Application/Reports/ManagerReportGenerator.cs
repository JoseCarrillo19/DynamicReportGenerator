using DynamicReportGenerator.Domain.Entities;
using DynamicReportGenerator.Domain.Interfaces;
using OfficeOpenXml;

namespace DynamicReportGenerator.Application.Reports
{
    public class ManagerReportGenerator: IReportGenerator
    {
        public async Task<Report> GenerateReportAsync(Report reportData)
        {
            // Lógica para generar el informe de gerente
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using var package = new ExcelPackage();

            var worksheet = package.Workbook.Worksheets.Add("Informe Gerente");

            worksheet.Cells[1, 1].Value = reportData.Title;

            // Escribir total de ventas
            var totalSales = reportData.SalesData.Sum(s => s.Amount);
            worksheet.Cells[3, 1].Value = "Total de Ventas:";
            worksheet.Cells[3, 2].Value = totalSales;
            worksheet.Cells[3, 2].Style.Numberformat.Format = "#,##0.00";

            // Escribir ventas por región
            worksheet.Cells[5, 1].Value = "Ventas por Región";

            worksheet.Cells[6, 1].Value = "Región";
            worksheet.Cells[6, 2].Value = "Monto";

            int row = 7;
            var salesByRegion = reportData.SalesData
                .GroupBy(s => s.Region)
                .Select(g => new { Region = g.Key, Total = g.Sum(s => s.Amount) });

            foreach (var item in salesByRegion)
            {
                worksheet.Cells[row, 1].Value = item.Region;
                worksheet.Cells[row, 2].Value = item.Total;
                worksheet.Cells[row, 2].Style.Numberformat.Format = "#,##0.00";
                row++;
            }

            // Ajustar ancho de columnas
            worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

            // Guardar el archivo en memoria
            var fileBytes = package.GetAsByteArray();

            reportData.FileContent = fileBytes;
            reportData.FileName = "ManagerReport.xlsx";
            reportData.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            await Task.CompletedTask;
            return reportData;
        }
    }
}
