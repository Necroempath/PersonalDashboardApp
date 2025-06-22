using PersonalDashboardApp.FinanceModule.Models;

namespace PersonalDashboardApp.FinanceModule.Repositories;

public class InMemoryCategoryRepository : ICategoryRepository
{
    private static readonly List<string> _categories = new()
    {
        "Food", "Transport", "Utilities", "Entertainment", "Healthcare", "Other"
    };
    
    public IEnumerable<string> GetCategories()
    {
        return _categories;
    }

    public void AddCategory(string category)
    {
        _categories.Add(category);
    }
}