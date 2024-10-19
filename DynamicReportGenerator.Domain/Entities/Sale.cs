using System.ComponentModel.DataAnnotations;

namespace DynamicReportGenerator.Domain.Entities
{
    public class Sale
    {
        [Key]
        public int Id { get; set; }
        public string Region { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
