namespace DynamicReportGenerator.Domain.Entities
{
    public class Report
    {
        public string? Title { get; set; }
        public IEnumerable<Sale>? SalesData { get; set; }
        public string? ExecutiveSummary { get; set; }

        public byte[]? FileContent { get; set; }
        public string? FileName { get; set; }
        public string? ContentType { get; set; }
    }
}
