using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using PersonalDashboardApp.TaskModule.DTOs;
using PersonalDashboardApp.TaskModule.Models;
using PersonalDashboardApp.TaskModule.Models.Enums;
using PersonalDashboardApp.TaskModule.Presenters;

namespace PersonalDashboardApp.TaskModule.Views;

public partial class TaskView : UserControl, ITaskView
{
    public event Action<TaskInputDto>? AddTaskRequested;
    public event Action<TaskInputDto>? UpdateTaskRequested;
    public event Action? DeleteTaskRequested;
    public event Action<int>? ToggleCompleteRequested;

    public ObservableCollection<TaskItem> Tasks { get; } = new();
    public TaskItem SelectedTask { get; private set; }
    
    public TaskView()
    {
        InitializeComponent();
        DataContext = this;
        DeadlinePicker.PreviewKeyDown += (s, e) => e.Handled = true; //Disables keyword input for Date Picker control
    }

    public void SetPriorityOptions(IEnumerable<Priority> options)
    {
        PriorityComboBox.ItemsSource = options;
        PriorityComboBox.SelectedIndex = 0;
    }

    public TaskInputDto GetTaskInputDto()
    {
        return new TaskInputDto(TitleTextBox.Text, DeadlinePicker.SelectedDate, (Priority)PriorityComboBox.SelectedItem);
    }

    public void ClearInput()
    {
        TitleTextBox.Clear();
    }

    public void ShowError(string message)
    {
        MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
    }

    public void ShowInfo(string message)
    {
        MessageBox.Show(message, "Info", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    private void AddButton_Click(object sender, RoutedEventArgs e)
    {
        AddTaskRequested?.Invoke(GetTaskInputDto());
    }

    private void TaskDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        SelectedTask = TaskDataGrid.SelectedItem as TaskItem;
        UpdateButton.IsEnabled = DeleteButton.IsEnabled = SelectedTask != null;
        
        if (SelectedTask != null)
        {
            TitleTextBox.Text = SelectedTask.Title;
            DeadlinePicker.SelectedDate = SelectedTask.Deadline;
            PriorityComboBox.SelectedValue = SelectedTask.Priority;
        }
    }

    private void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        DeleteTaskRequested?.Invoke();
    }

    private void UpdateButton_Click(object sender, RoutedEventArgs e)
    {
        UpdateTaskRequested?.Invoke(GetTaskInputDto());
    }
}