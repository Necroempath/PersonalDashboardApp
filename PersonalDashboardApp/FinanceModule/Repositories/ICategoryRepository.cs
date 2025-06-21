using PersonalDashboardApp.FinanceModule.Models;

namespace PersonalDashboardApp.FinanceModule.Repositories;

public interface ICategoryRepository
{
    public IEnumerable<Category> GetCategories();
}