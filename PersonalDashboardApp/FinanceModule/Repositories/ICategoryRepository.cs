using PersonalDashboardApp.FinanceModule.Models;

namespace PersonalDashboardApp.FinanceModule.Repositories;

public interface ICategoryRepository
{
    public IEnumerable<string> GetCategories();
    
    public void AddCategory(string category);
}