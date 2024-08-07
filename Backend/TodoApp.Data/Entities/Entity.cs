using System.ComponentModel.DataAnnotations;

namespace TodoApp.Data.Entities
{
    public class Entity : AuditFields
    {
        [Key]
        public int Id { get; set; }
    }
}
