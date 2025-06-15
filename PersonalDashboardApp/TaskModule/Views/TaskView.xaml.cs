using System.Windows;
using System.Windows.Controls;
using PersonalDashboardApp.TaskModule.DTOs;
using PersonalDashboardApp.TaskModule.Models;
using PersonalDashboardApp.TaskModule.Models.Enums;

namespace PersonalDashboardApp.TaskModule.Views;

public partial class TaskView : UserControl, ITaskView
{
    public event Action<TaskInputDto>? AddTaskRequested;
    public event Action<TaskInputDto>? UpdateTaskRequested;
    public event Action<int>? DeleteTaskRequested;
    public event Action<int>? ToggleCompleteRequested;
    
    public TaskView()
    {
        InitializeComponent();
        
        // AddTaskButton.Click += (s, e) => {
        //     
        //     TaskInputDto task = new()
        //     {
        //         Title = "Title",
        //         Deadline = DateTime.Now,
        //         Priority = Priority.High
        //     };
        //     
        //     AddTaskRequested?.Invoke(task);
        // };
    }


    public void SetTaskList(IEnumerable<TaskItem> tasks)
    {
//        TaskListBox.ItemsSource = tasks;
    }

    public void ShowError(string message)
    {
        MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
    }

    public void ShowInfo(string message)
    {
        MessageBox.Show(message, "Info", MessageBoxButton.OK, MessageBoxImage.Information);
    }
}