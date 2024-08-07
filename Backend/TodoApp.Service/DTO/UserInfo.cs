using System.ComponentModel.DataAnnotations;

namespace TodoApp.Service.DTO
{
    public class UserInfo
    {
        [Required]
        public string? UserName { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
