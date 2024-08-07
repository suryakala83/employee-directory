using System.ComponentModel.DataAnnotations;

namespace TodoApp.Data.Entities
{
    public class AuditFields
    {
        [Required]
        public string? CreatedBy { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedAt { get; set; }
    }
}
