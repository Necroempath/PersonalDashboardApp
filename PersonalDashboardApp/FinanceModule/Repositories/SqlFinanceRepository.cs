using PersonalDashboardApp.FinanceModule.Models;
using PersonalDashboardApp.TaskModule.Data;

namespace PersonalDashboardApp.FinanceModule.Repositories;

public class SqlFinanceRepository(ApplicationDbContext dbContext) : IFinanceRepository
{
    public void AddRecord(FinanceRecord record)
    {
        dbContext.FinanceRecords.Add(record);
        dbContext.SaveChanges();
    }

    public void UpdateRecord(FinanceRecord record)
    {
        dbContext.FinanceRecords.Update(record);
        dbContext.SaveChanges();
    }

    public void DeleteRecord(int id)
    {
        dbContext.FinanceRecords.Remove(dbContext.FinanceRecords.Find(id));
        dbContext.SaveChanges();
    }

    public IEnumerable<FinanceRecord> GetAllRecords()
    {
        return dbContext.FinanceRecords.ToList();
    }
}