using System.ComponentModel.DataAnnotations;

namespace TodoApp.Service.DTO
{
    public class TaskItem
    {
        [Required]
        public string? Name { get; set; }

        public string? Description { get; set; }

        [Required]
        public DateTime DueDate { get; set; }
    }
}
