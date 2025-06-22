using PersonalDashboardApp.TaskModule.DTOs;

namespace PersonalDashboardApp.TaskModule.Models;

public interface ITaskRepository
{
    void AddTask(TaskItem task);
    void UpdateTask(TaskItem task);
    void DeleteTask(int id);
    IEnumerable<TaskItem> GetAllTasks();
}