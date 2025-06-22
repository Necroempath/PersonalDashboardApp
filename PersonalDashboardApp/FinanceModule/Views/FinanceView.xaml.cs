using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using PersonalDashboardApp.FinanceModule.Converters;
using PersonalDashboardApp.FinanceModule.Models;
using PersonalDashboardApp.FinanceModule.Models.DTOs;
using PersonalDashboardApp.FinanceModule.Models.Enums;

namespace PersonalDashboardApp.FinanceModule.Views;

public partial class FinanceView : UserControl, IFinanceView
{
    private FinanceInputDTO _financeInputDto = new() { Note = string.Empty };

    public event Action<FinanceInputDTO>? AddFinanceRecordRequested;
    public event Action<FinanceInputDTO>? UpdateFinanceRecordRequested;
    public event Action<int>? DeleteFinanceRecordRequested;

    public ObservableCollection<FinanceRecord> FinanceRecords { get; } = new();
    public FinanceRecord SelectedFinanceRecord { get; set; }

    public FinanceView()
    {
        InitializeComponent();
        DataContext = _financeInputDto;
        TransactionTimeDatePicker.PreviewKeyDown += (s, e) => e.Handled = true;
    }

    private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
    {
        DeleteFinanceRecordRequested?.Invoke(SelectedFinanceRecord.Id);
    }

    private void UpdateButton_OnClick(object sender, RoutedEventArgs e)
    {
        UpdateFinanceRecordRequested?.Invoke(_financeInputDto);
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        AddFinanceRecordRequested?.Invoke(_financeInputDto);
    }

    public void SetCategoryTypes(IEnumerable<string> categories)
    {
        _financeInputDto.Category = categories.First();
        CategoryComboBox.ItemsSource = categories;
        CategoryComboBox.SelectedIndex = 0;
    }

    public void SetRecords(IEnumerable<FinanceRecord> records)
    {
        foreach (var record in records)
        {
            FinanceRecords.Add(record);
        }
    }

    public void ClearInput()
    {
        AmountTextBox.Text = string.Empty;
        NoteTextBox.Text = string.Empty;

        _financeInputDto.Amount = string.Empty;
        _financeInputDto.Note = string.Empty;
    }

    private void FinanceRecordsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        SelectedFinanceRecord = DataGridFinanceRecords.SelectedItem as FinanceRecord;
        UpdateButton.IsEnabled = DeleteButton.IsEnabled = SelectedFinanceRecord != null;

        if (SelectedFinanceRecord != null)
        {
            // var converter = new EnumToBooleanConverter();
            // IncomeRadioButton.IsChecked = converter();
            _financeInputDto.Amount = AmountTextBox.Text = SelectedFinanceRecord.Amount.ToString();
            CategoryComboBox.SelectedItem = SelectedFinanceRecord.Category;
            TransactionTimeDatePicker.SelectedDate = SelectedFinanceRecord.TransactionDate;
            _financeInputDto.Note = NoteTextBox.Text = SelectedFinanceRecord.Note;
        }
    }
}