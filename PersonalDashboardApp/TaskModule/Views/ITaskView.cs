using System.Collections.ObjectModel;
using PersonalDashboardApp.TaskModule.DTOs;
using PersonalDashboardApp.TaskModule.Models;
using PersonalDashboardApp.TaskModule.Models.Enums;

namespace PersonalDashboardApp.TaskModule.Views;

public interface ITaskView
{
    event Action<TaskInputDto> AddTaskRequested;
    event Action<TaskInputDto> UpdateTaskRequested;
    event Action DeleteTaskRequested;
    event Action<int> ToggleCompleteRequested;
    
   // void SetTasksList(IEnumerable<TaskItem> tasks);
    void ShowError(string message);
    void ShowInfo(string message);
    public void SetPriorityOptions(IEnumerable<Priority> options);
    public void SetTasks(IEnumerable<TaskItem> taksk);
   // public TaskInputDto GetTaskInputDto();

    void ClearInput();
    
    public ObservableCollection<TaskItem> Tasks { get; }
    
    public TaskItem SelectedTask { get; }
}