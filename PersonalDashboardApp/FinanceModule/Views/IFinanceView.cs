using System.Collections.ObjectModel;
using PersonalDashboardApp.FinanceModule.Models;
using PersonalDashboardApp.FinanceModule.Models.DTOs;

namespace PersonalDashboardApp.FinanceModule.Views;

public interface IFinanceView
{
    public event Action<FinanceInputDto> AddFinanceRecordRequested;
    public event Action<FinanceInputDto> UpdateFinanceRecordRequested;
    public event Action<int> DeleteFinanceRecordRequested;
    public event Action<string, TransactionTypeFilter>? SearchRequested;
    public event Func<BalanceInfo> CalculateBalanceRequested;
    public event Action<TransactionTypeFilter> TransactionTypeFilterOptionChanged;

    public void SetCategoryTypes(IEnumerable<string> categories);

    public void SetTransactionTypeFilterOptions(IEnumerable<TransactionTypeFilter> filterOptions);
    
    public void SetRecords(IEnumerable<FinanceRecord> records);
    
    public ObservableCollection<FinanceRecord> FinanceRecords { get; }
    
    public FinanceRecord SelectedFinanceRecord { get; }

    void ClearInput();
    void ClearFilterTextBox();
    void ShowError(string message);
}
