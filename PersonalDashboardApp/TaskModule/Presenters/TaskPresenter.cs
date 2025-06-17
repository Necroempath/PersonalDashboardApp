using PersonalDashboardApp.TaskModule.DTOs;
using PersonalDashboardApp.TaskModule.Models;
using PersonalDashboardApp.TaskModule.Models.Enums;
using PersonalDashboardApp.TaskModule.Views;

namespace PersonalDashboardApp.TaskModule.Presenters;

public class TaskPresenter
{
    private readonly ITaskView _view;
    private readonly ITaskRepository _repository;

    public TaskPresenter(ITaskView view, ITaskRepository repository)
    {
        _view = view;
        _repository = repository;

        _view.AddTaskRequested += OnAddTaskRequested;
        _view.UpdateTaskRequested += OnUpdateTaskRequested;
        _view.DeleteTaskRequested += OnDeleteTaskRequested;
        _view.ToggleCompleteRequested += OnToggleCompleteRequested;
        
        _view.SetPriorityOptions(Enum.GetValues(typeof(Priority)).Cast<Priority>());
    }

    private void OnToggleCompleteRequested(int index)
    {
        throw new NotImplementedException();
    }

    private void OnDeleteTaskRequested()
    {
        _repository.DeleteTask(_view.SelectedTask.Id);
        _view.Tasks.Remove(_view.SelectedTask);
        _view.ClearInput();
    }

    private void OnUpdateTaskRequested(TaskInputDto task)
    {
        var errors = Validate(task);
        
        if (errors.Count != 0)
        {
            _view.ShowError(string.Join('\n', errors));
            return;
        }
        
        TaskItem item = _view.SelectedTask;
        item.Title = task.Title;
        item.Deadline = task.Deadline!.Value;
        item.Priority = task.Priority;
        
        _repository.UpdateTask(item);
        _view.Tasks.Remove(_view.SelectedTask);
        _view.Tasks.Add(item);
        _view.ClearInput();
    }

    private void OnAddTaskRequested(TaskInputDto task)
    {
        var errors = Validate(task);
        
        if (errors.Count != 0)
        {
            _view.ShowError(string.Join('\n', errors));
            return;
        }

        TaskItem item = new TaskItem { Title = task.Title, Deadline = task.Deadline!.Value, Priority = task.Priority };
        
        _repository.AddTask(item);
        _view.Tasks.Add(item);
        _view.ClearInput();
    }

    private List<string> Validate(TaskInputDto task)
    {
        List<string> errors = new();
        if (string.IsNullOrEmpty(task.Title))
        {
            errors.Add("Title cannot be empty");
        }

        if (task.Deadline == null)
        {
            errors.Add("Deadline cannot be empty");
        }
        else if (task.Deadline < DateTime.Now)
        {
            errors.Add("Deadline cannot be less than today");
        }
        
        return errors;
    }
}