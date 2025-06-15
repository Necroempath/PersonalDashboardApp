using PersonalDashboardApp.TaskModule.DTOs;
using PersonalDashboardApp.TaskModule.Models;
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
    }

    private void OnToggleCompleteRequested(int obj)
    {
        throw new NotImplementedException();
    }

    private void OnDeleteTaskRequested(int obj)
    {
        throw new NotImplementedException();
    }

    private void OnUpdateTaskRequested(TaskInputDto obj)
    {
        throw new NotImplementedException();
    }

    private void OnAddTaskRequested(TaskInputDto task)
    {
        var errors = Validate(task);
        
        if (errors.Count != 0)
        {
            _view.ShowError(string.Join('\n', errors));
            return;
        }
        
        _repository.AddTask(task);
        
    }

    private List<string> Validate(TaskInputDto task)
    {
        List<string> errors = new();
        if (string.IsNullOrEmpty(task.Title))
        {
            errors.Add("Title cannot be empty");
        }

        if (task.Deadline < DateTime.Now)
        {
            errors.Add("Deadline cannot be less than today");
        }
        
        return errors;
    }
}