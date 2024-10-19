using DynamicReportGenerator.Domain.Entities;
using DynamicReportGenerator.Domain.Interfaces;
using OfficeOpenXml.Drawing.Chart;
using OfficeOpenXml;

namespace DynamicReportGenerator.Application.Reports
{
    public class DirectorReportGenerator : IReportGenerator
    {
        public async Task<Report> GenerateReportAsync(Report reportData)
        {
            // Lógica para generar el informe de director
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using var package = new ExcelPackage();

            var worksheet = package.Workbook.Worksheets.Add("Informe Director");

            worksheet.Cells[1, 1].Value = reportData.Title;

            // Escribir resumen ejecutivo
            worksheet.Cells[3, 1].Value = "Resumen Ejecutivo:";
            worksheet.Cells[4, 1].Value = reportData.ExecutiveSummary;

            // Escribir total y promedio de ventas
            var totalSales = reportData.SalesData.Sum(s => s.Amount);
            var averageSales = reportData.SalesData.Average(s => s.Amount);

            worksheet.Cells[6, 1].Value = "Total de Ventas:";
            worksheet.Cells[6, 2].Value = totalSales;
            worksheet.Cells[6, 2].Style.Numberformat.Format = "#,##0.00";

            worksheet.Cells[7, 1].Value = "Promedio de Ventas:";
            worksheet.Cells[7, 2].Value = averageSales;
            worksheet.Cells[7, 2].Style.Numberformat.Format = "#,##0.00";

            // Crear gráfico de ventas por región
            var salesByRegion = reportData.SalesData
                .GroupBy(s => s.Region)
                .Select(g => new { Region = g.Key, Total = g.Sum(s => s.Amount) })
                .ToList();

            worksheet.Cells[9, 1].Value = "Ventas por Región";
            worksheet.Cells[10, 1].Value = "Región";
            worksheet.Cells[10, 2].Value = "Monto";

            int row = 11;
            foreach (var item in salesByRegion)
            {
                worksheet.Cells[row, 1].Value = item.Region;
                worksheet.Cells[row, 2].Value = item.Total;
                worksheet.Cells[row, 2].Style.Numberformat.Format = "#,##0.00";
                row++;
            }

            // Crear el gráfico
            var chart = worksheet.Drawings.AddChart("SalesChart", eChartType.PieExploded3D) as ExcelPieChart;
            chart.Title.Text = "Ventas por Región";
            chart.SetPosition(10, 0, 3, 0);
            chart.SetSize(600, 400);

            chart.Series.Add(worksheet.Cells[11, 2, row - 1, 2], worksheet.Cells[11, 1, row - 1, 1]);

            // Ajustar ancho de columnas
            worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

            // Guardar el archivo en memoria
            var fileBytes = package.GetAsByteArray();

            reportData.FileContent = fileBytes;
            reportData.FileName = "DirectorReport.xlsx";
            reportData.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            await Task.CompletedTask;
            return reportData;
        }
    }
}
