using System.Collections.ObjectModel;
using System.Transactions;
using PersonalDashboardApp.FinanceModule.Models;
using PersonalDashboardApp.FinanceModule.Models.DTOs;

namespace PersonalDashboardApp.FinanceModule.Views;

public interface IFinanceView
{
    public event Action<FinanceInputDTO> AddFinanceRecordRequested;
    public event Action<FinanceInputDTO> UpdateFinanceRecordRequested;
    public event Action<int> DeleteFinanceRecordRequested;
    
    public void SetCategoryTypes(IEnumerable<string> categories);
    
    public void SetRecords(IEnumerable<FinanceRecord> records);
    
    public ObservableCollection<FinanceRecord> FinanceRecords { get; }
    
    public FinanceRecord SelectedFinanceRecord { get; }
    void ClearInput();
}
