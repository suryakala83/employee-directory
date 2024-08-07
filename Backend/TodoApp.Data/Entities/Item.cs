using System.ComponentModel.DataAnnotations;

namespace TodoApp.Data.Entities
{
    public class Item : Entity
    {
        [Required]
        public string? Name { get; set; }

        public string? Description { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        public bool IsCompleted { get; set; } = false;

        [Required]
        public string? AssignedTo { get; set; }

        public User? User { get; set; }
    }
}
