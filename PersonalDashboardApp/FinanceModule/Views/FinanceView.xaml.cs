using System.Windows;
using System.Windows.Controls;
using PersonalDashboardApp.FinanceModule.Models;
using PersonalDashboardApp.FinanceModule.Models.DTOs;
using PersonalDashboardApp.FinanceModule.Models.Enums;

namespace PersonalDashboardApp.FinanceModule.Views;

public partial class FinanceView : UserControl, IFinanceView
{
    private FinanceInputDTO _financeInputDto = new();
    
    public event Action<FinanceInputDTO>? AddFinanceRecordRequested;
    public event Action<FinanceInputDTO>? UpdateFinanceRecordRequested;
    public event Action<int>? DeleteFinanceRecordRequested;

    public FinanceView()
    {
        InitializeComponent();
        DataContext = _financeInputDto;
        TransactionTimeDatePicker.PreviewKeyDown += (s, e) => e.Handled = true;
    }

    private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void UpdateButton_OnClick(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        AddFinanceRecordRequested?.Invoke(_financeInputDto);
    }

    public void SetCategoryTypes(IEnumerable<Category> categories)
    {
        _financeInputDto.Category = categories.First();
        CategoryComboBox.ItemsSource = categories;
        CategoryComboBox.SelectedIndex = 0;
    }
}