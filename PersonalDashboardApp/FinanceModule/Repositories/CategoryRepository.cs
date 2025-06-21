using PersonalDashboardApp.FinanceModule.Models;

namespace PersonalDashboardApp.FinanceModule.Repositories;

public class InMemoryCategoryRepository : ICategoryRepository
{
    public IEnumerable<Category> GetCategories()
    {
        return new List<Category>
        {
            new("Food"),
            new("Transport"),
            new("Utilities"),
            new("Entertainment"),
            new("Healthcare"),
            new("Education")
        };
    }
}