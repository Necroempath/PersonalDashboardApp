using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using PersonalDashboardApp.FinanceModule.Converters;
using PersonalDashboardApp.FinanceModule.Models;
using PersonalDashboardApp.FinanceModule.Models.DTOs;
using PersonalDashboardApp.FinanceModule.Models.Enums;

namespace PersonalDashboardApp.FinanceModule.Views;

public partial class FinanceView : UserControl, IFinanceView
{
    private FinanceInputDto _financeInputDto = new() { Note = string.Empty };

    public event Action<FinanceInputDto>? AddFinanceRecordRequested;
    public event Action<FinanceInputDto>? UpdateFinanceRecordRequested;
    public event Action<int>? DeleteFinanceRecordRequested;
    public event Action<string, TransactionTypeFilter>? SearchRequested;
    public event Func<BalanceInfo>? CalculateBalanceRequested;
    public event Action<TransactionTypeFilter>? TransactionTypeFilterOptionChanged;

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

    private void AddButton_OnClick(object sender, RoutedEventArgs e)
    {
        AddFinanceRecordRequested?.Invoke(_financeInputDto);
    }

    public void SetTransactionTypeFilterOptions(IEnumerable<TransactionTypeFilter> filterOptions)
    {
        TransactionTypeFilterComboBox.ItemsSource = filterOptions;
        TransactionTypeFilterComboBox.SelectedIndex = 0;
    }
    
    public void SetCategoryTypes(IEnumerable<string> categories)
    {
        _financeInputDto.Category = categories.First();
        CategoryComboBox.ItemsSource = categories;
        CategoryComboBox.SelectedIndex = 0;
    }

    public void SetRecords(IEnumerable<FinanceRecord> records)
    {
        FinanceRecords.Clear();

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

    public void ClearFilterTextBox()
    {
        FilterTextBox.Clear();
    }

    public void ShowError(string message)
    {
        MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
    }

    private void FinanceRecordsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        SelectedFinanceRecord = DataGridFinanceRecords.SelectedItem as FinanceRecord;
        UpdateButton.IsEnabled = DeleteButton.IsEnabled = SelectedFinanceRecord != null;

        if (SelectedFinanceRecord != null)
        {
            _financeInputDto.Amount = AmountTextBox.Text = SelectedFinanceRecord.Amount.ToString();
            CategoryComboBox.SelectedItem = SelectedFinanceRecord.Category;
            TransactionTimeDatePicker.SelectedDate = SelectedFinanceRecord.TransactionDate;
            _financeInputDto.Note = NoteTextBox.Text = SelectedFinanceRecord.Note;
        }
    }

    private void FilterTextBox_OnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            SearchRequested?.Invoke((sender as TextBox).Text, TransactionTypeFilterComboBox.SelectedItem as TransactionTypeFilter);
        }
    }

    private void SearchButton_OnClick(object sender, RoutedEventArgs e)
    {
        SearchRequested?.Invoke(FilterTextBox.Text, TransactionTypeFilterComboBox.SelectedItem as TransactionTypeFilter);
    }

    private void BalanceInfoButton_OnClick(object sender, RoutedEventArgs e)
    {
        var balanceInfo = CalculateBalanceRequested?.Invoke();
        var message = $"""
                         Incomes sum: {balanceInfo.IncomesSum}
                         Expenses sum: {balanceInfo.ExpensesSum}
                         Total balance: {balanceInfo.TotalBalance}
                       """;
        
        MessageBox.Show(message, "Balance Info", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    private void TransactionTypeFilterComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        TransactionTypeFilterOptionChanged?.Invoke(TransactionTypeFilterComboBox.SelectedItem as TransactionTypeFilter);
    }
}