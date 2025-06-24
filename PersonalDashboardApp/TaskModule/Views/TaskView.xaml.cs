using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using PersonalDashboardApp.TaskModule.DTOs;
using PersonalDashboardApp.TaskModule.Models;
using PersonalDashboardApp.TaskModule.Models.Enums;

namespace PersonalDashboardApp.TaskModule.Views;

public partial class TaskView : ITaskView
{
    public event Action<TaskInputDto>? AddTaskRequested;
    public event Action<TaskInputDto>? UpdateTaskRequested;
    public event Action? DeleteTaskRequested;
    public event Action<string, bool?>? SearchRequested;
    public event Action<bool?>? StatusFilterChanged;
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

    public void SetStatusFilterOptions(IEnumerable<StatusFilter> options)
    {
        StatusFilterComboBox.ItemsSource = options;
        StatusFilterComboBox.SelectedIndex = 0;
    }
    
    public void SetTasks(IEnumerable<TaskItem> tasks)
    {
        Tasks.Clear();
        
        foreach (var task in tasks)
        {
            Tasks.Add(task);
        }
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

    private void SearchButton_OnClick(object sender, RoutedEventArgs e)
    {
        SearchRequested?.Invoke(FilterTextBox.Text, (StatusFilterComboBox.SelectedItem as StatusFilter).Value);
    }

    private void FilterTextBox_OnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            SearchRequested?.Invoke(FilterTextBox.Text, (StatusFilterComboBox.SelectedItem as StatusFilter).Value);
        }
    }

    public void ClearSearchTextBox()
    {
        FilterTextBox.Clear();
    }

    private void StatusFilterComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        StatusFilterChanged?.Invoke((StatusFilterComboBox.SelectedItem as StatusFilter).Value);
    }
}