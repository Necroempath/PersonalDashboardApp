using PersonalDashboardApp.TaskModule.Models.Enums;

namespace PersonalDashboardApp.TaskModule.DTOs;

public class TaskInputDto
{
    public required string Title { get; init; }
    public required DateTime Deadline { get; init; }
    public required Priority Priority { get; init; }
}