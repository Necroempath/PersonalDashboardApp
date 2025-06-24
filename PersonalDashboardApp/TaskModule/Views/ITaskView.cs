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
    event Action<string, bool?> SearchRequested;
    event Action<bool?> StatusFilterChanged;
    
    public void SetPriorityOptions(IEnumerable<Priority> options);
    public void SetStatusFilterOptions(IEnumerable<StatusFilter> options);
    public void SetTasks(IEnumerable<TaskItem> tasks);

    void ShowError(string message);
    void ClearInput();
    void ClearSearchTextBox();

    public ObservableCollection<TaskItem> Tasks { get; }
    
    public TaskItem SelectedTask { get; }
}