using DynamicReportGenerator.Domain.Entities.Enum;
using System.ComponentModel.DataAnnotations;

namespace DynamicReportGenerator.Domain.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public UserRole Role { get; set; }
    }
}
