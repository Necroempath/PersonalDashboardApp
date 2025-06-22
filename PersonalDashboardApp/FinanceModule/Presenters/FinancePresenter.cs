using System.Windows;
using PersonalDashboardApp.FinanceModule.Models;
using PersonalDashboardApp.FinanceModule.Models.DTOs;
using PersonalDashboardApp.FinanceModule.Repositories;
using PersonalDashboardApp.FinanceModule.Views;

namespace PersonalDashboardApp.FinanceModule.Presenters;

public class FinancePresenter
{
    private IFinanceView _view;
    private IFinanceRepository _repository;
    private ICategoryRepository _categoryRepository;
    
    public FinancePresenter(IFinanceView view, IFinanceRepository repository, ICategoryRepository categoryRepository)
    {
        _view = view;
        _repository = repository;
        _categoryRepository = categoryRepository;
        
        _view.SetRecords(_repository.GetAllRecords());
        _view.SetCategoryTypes(_categoryRepository.GetCategories());
        
        _view.AddFinanceRecordRequested += OnAddFinanceRecordRequested;
        _view.DeleteFinanceRecordRequested += OnDeleteFinanceRequested;
        _view.UpdateFinanceRecordRequested += OnUpdateFinanceRequested;
    }

    private void OnDeleteFinanceRequested(int recordId)
    {
        _repository.DeleteRecord(recordId);
        _view.FinanceRecords.Remove(_view.SelectedFinanceRecord);
    }

    private void OnUpdateFinanceRequested(FinanceInputDTO input)
    {
        var errors = Validate(input);

        if (errors.Any())
        {
            MessageBox.Show($"Errors : {errors}");
            return;
        }
        
        FinanceRecord record = _view.SelectedFinanceRecord;
        
        record.TransactionType = input.Type;
        record.Amount = decimal.Parse(input.Amount);
        record.Category = input.Category;
        record.TransactionDate = input.Date.Value;
        record.Note = input.Note;
        
        _repository.UpdateRecord(record);

        _view.FinanceRecords.Remove(_view.SelectedFinanceRecord);
        _view.FinanceRecords.Add(record);
        _view.ClearInput();
    }

    private void OnAddFinanceRecordRequested(FinanceInputDTO input)
    {
        var errors = Validate(input);

        if (errors.Any())
        {
            MessageBox.Show("There were errors while adding finance record");
            return;
        }
        
        FinanceRecord record = new() {TransactionType = input.Type, Amount = decimal.Parse(input.Amount), Category = input.Category, TransactionDate = input.Date!.Value, Note = input.Note};
        
        _repository.AddRecord(record);
        _view.FinanceRecords.Add(record);
        
        _view.ClearInput();
    }

    private List<string> Validate(FinanceInputDTO input)
    {
        List<string> errors = new();

        if (!decimal.TryParse(input.Amount, out decimal amount))
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