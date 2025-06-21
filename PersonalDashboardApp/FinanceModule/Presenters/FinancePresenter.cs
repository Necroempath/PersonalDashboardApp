using System.Windows;
using PersonalDashboardApp.FinanceModule.Models.DTOs;
using PersonalDashboardApp.FinanceModule.Repositories;
using PersonalDashboardApp.FinanceModule.Views;

namespace PersonalDashboardApp.FinanceModule.Presenters;

public class FinancePresenter
{
    private IFinanceView _view;
    private ICategoryRepository _categoryRepository;
    
    public FinancePresenter(IFinanceView view, ICategoryRepository categoryRepository)
    {
        _view = view;
        _categoryRepository = categoryRepository;
        
        _view.SetCategoryTypes(_categoryRepository.GetCategories());
        _view.AddFinanceRecordRequested += OnAddFinanceRecordRequested;
    }

    private void OnAddFinanceRecordRequested(FinanceInputDTO input)
    {
        var errors = Validate(input);

        if (errors.Any())
        {
            MessageBox.Show("There were errors while adding finance record");
        }
        
        MessageBox.Show($"{input.Category.Name} Finance record has been added");
    }

    private List<string> Validate(FinanceInputDTO input)
    {
        List<string> errors = new();

        if (!float.TryParse(input.Amount, out float amount))
        {
            errors.Add("Amount must be a float");
        }
        else if (amount < 0)
        {
            errors.Add("Amount must be positive number");
        }

        if (input.Date is null)
        {
            errors.Add("Transaction date can not be empty");
        }
        else if (input.Date < DateTime.Now)
        {
            errors.Add("Transaction date can not be less than today");
        }
        
        return errors;
    }
}