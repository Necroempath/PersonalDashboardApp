using System.ComponentModel.DataAnnotations;
using PersonalDashboardApp.TaskModule.Models.Enums;

namespace PersonalDashboardApp.TaskModule.Models;

public class TaskItem
{
    [Display(Name = "Task Id")]
    public int Id { get; init; }
    
    [Display(Name = "Task Title")]
    [Required(ErrorMessage = "Task Title is required")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Task Title must be between 3 and 50 characters")]
    public required string Title { get; set; }
    
    [Display(Name = "Task Priority")]
    public Priority Priority { get; set; }
    
    [Display(Name = "Task Deadline")]
    public DateTime Deadline { get; set; }
    
    [Display(Name = "Task Status")]
    public bool IsCompleted { get; private set; }

    public void Complete()
    {
        IsCompleted = true;
    }

    public void Resume()
    {
        IsCompleted = false;
    }
}