using PersonalDashboardApp.TaskModule.DTOs;
using PersonalDashboardApp.TaskModule.Models;

namespace PersonalDashboardApp.TaskModule.Views;

public interface ITaskView
{
    event Action<TaskInputDto> AddTaskRequested;
    event Action<TaskInputDto> UpdateTaskRequested;
    event Action<int> DeleteTaskRequested;
    event Action<int> ToggleCompleteRequested;
    
    void SetTaskList(IEnumerable<TaskItem> tasks);
    void ShowError(string message);
    void ShowInfo(string message);
}