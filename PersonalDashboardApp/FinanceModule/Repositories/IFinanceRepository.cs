using PersonalDashboardApp.FinanceModule.Models;

namespace PersonalDashboardApp.FinanceModule.Repositories;

public interface IFinanceRepository
{
    public void AddRecord(FinanceRecord record);
    
    public void UpdateRecord(FinanceRecord record);
    
    public void DeleteRecord(int id);
    
    IEnumerable<FinanceRecord> GetAllRecords();
}