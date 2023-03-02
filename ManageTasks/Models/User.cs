using System.ComponentModel.DataAnnotations;

namespace ManageTasks.Models;

public class User : Base{
    public string Name { get; set; }
    public int Age { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public string Email { get; set; }
}