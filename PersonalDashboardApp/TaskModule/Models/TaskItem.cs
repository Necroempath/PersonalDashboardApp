using System.ComponentModel.DataAnnotations;
using PersonalDashboardApp.TaskModule.Models.Enums;

namespace PersonalDashboardApp.TaskModule.Models;

public class TaskItem
{
    [Key]
    public int Id { get; init; }
    
    public required string Title { get; set; }
    
    public required Priority Priority { get; set; }
    
    public required DateTime Deadline { get; set; }
    
    public bool IsCompleted { get; set; }
    
    public string Status => IsCompleted ? "Completed" : "Active";
}