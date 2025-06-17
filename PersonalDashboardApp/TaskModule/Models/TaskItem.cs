using System.ComponentModel.DataAnnotations;
using PersonalDashboardApp.TaskModule.Models.Enums;

namespace PersonalDashboardApp.TaskModule.Models;

public class TaskItem
{
    [Display(Name = "Task Id")]
    [Key]
    public int Id { get; init; }
    
    [Display(Name = "Task Title")]
    [Required(ErrorMessage = "Task Title is required")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Task Title must be between 3 and 50 characters")]
    public required string Title { get; set; }
    
    [Display(Name = "Task Priority")]
    public required Priority Priority { get; set; }
    
    [Display(Name = "Task Deadline")]
    public required DateTime Deadline { get; set; }
    
    [Display(Name = "Task Status")]
    public bool IsCompleted { get; set; }
    
    [Display(Name = "Status")]
    public string Status => IsCompleted ? "Completed" : "Active";
}