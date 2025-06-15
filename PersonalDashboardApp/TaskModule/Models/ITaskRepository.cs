using PersonalDashboardApp.TaskModule.DTOs;

namespace PersonalDashboardApp.TaskModule.Models;

public interface ITaskRepository
{
    void AddTask(TaskInputDto task);
    void UpdateTask(TaskInputDto task);
    void DeleteTask(int id);
    IEnumerable<TaskItem> GetAllTasks();
    IEnumerable<TaskItem> GetTasksByTitle(string title);
}