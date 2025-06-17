using PersonalDashboardApp.TaskModule.Models.Enums;

namespace PersonalDashboardApp.TaskModule.DTOs;

public record TaskInputDto(string Title, DateTime? Deadline, Priority Priority);