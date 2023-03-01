using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ManageTasks.Models.Enums;

namespace ManageTasks.Models;

public class Tasks : Base{
    [Required]
    public string Title { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; }
    public Status Status { get; set; }
    [ForeignKey("User")]
    public int UserId { get; set; }

    public bool DateTaskValid(DateTime date){
        if(date <= DateTime.Now || date > DateTime.Now.AddYears(2)){
            return false;
        }
        return true;
    }
}