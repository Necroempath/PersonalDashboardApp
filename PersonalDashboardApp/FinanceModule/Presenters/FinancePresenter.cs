using System.Windows;
using PersonalDashboardApp.FinanceModule.Models;
using PersonalDashboardApp.FinanceModule.Models.DTOs;
using PersonalDashboardApp.FinanceModule.Models.Enums;
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
        _view.SetTransactionTypeFilterOptions(GetTransactionTypeFilterOptions());
        
        _view.AddFinanceRecordRequested += OnAddFinanceRecordRequested;
        _view.DeleteFinanceRecordRequested += OnDeleteFinanceRequested;
        _view.UpdateFinanceRecordRequested += OnUpdateFinanceRequested;
        _view.SearchRequested += OnSearchRequested;
        _view.CalculateBalanceRequested += OnCalculateBalanceRequested;
        _view.TransactionTypeFilterOptionChanged += OnTransactionTypeFilterOptionChanged;
    }

    private void OnTransactionTypeFilterOptionChanged(TransactionTypeFilter filter)
    {
        _view.SetRecords(GetFilteredRecords(filter));
    }

    private BalanceInfo OnCalculateBalanceRequested()
    {
        decimal incomesSum = _repository.GetAllRecords().Where(fr => fr.TransactionType == TransactionType.Income).Sum(fr => fr.Amount);
        decimal expensesSum = _repository.GetAllRecords().Where(fr => fr.TransactionType == TransactionType.Expense).Sum(fr => fr.Amount);
        
        return new BalanceInfo(incomesSum, expensesSum, incomesSum - expensesSum);
    }


    private void OnSearchRequested(string keyword, TransactionTypeFilter filter)
    {
        _view.SetRecords(GetFilteredRecords(filter).Where(r => r.Note.ToLower().Contains(keyword.ToLower())));
        _view.ClearFilterTextBox();
    }

    private IEnumerable<FinanceRecord> GetFilteredRecords(TransactionTypeFilter filter)
    {
        var filteredRecords = _repository.GetAllRecords();
        
        return filter.Value is null ? filteredRecords : filteredRecords.Where(fr => fr.TransactionType == filter.Value);        
    }

    private void OnDeleteFinanceRequested(int recordId)
    {
        _repository.DeleteRecord(recordId);
        _view.FinanceRecords.Remove(_view.SelectedFinanceRecord);
    }

    private void OnUpdateFinanceRequested(FinanceInputDto input)
    {
        var errors = Validate(input);

        if (errors.Any())
        {
            _view.ShowError(string.Join('\n', errors));
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

    private void OnAddFinanceRecordRequested(FinanceInputDto input)
    {
        var errors = Validate(input);

        if (errors.Any())
        {
            _view.ShowError(string.Join('\n', errors));
            return;
        }
        
        FinanceRecord record = new() {TransactionType = input.Type, Amount = decimal.Parse(input.Amount), Category = input.Category, TransactionDate = input.Date!.Value, Note = input.Note};
        
        _repository.AddRecord(record);
        _view.FinanceRecords.Add(record);
        
        _view.ClearInput();
    }

    private List<string> Validate(FinanceInputDto input)
    {
        List<string> errors = new();

        if (!decimal.TryParse(input.Amount, out decimal amount))
        {
            errors.Add("Amount must be positive number");
        }
        else if (amount < 0)
        {
            errors.Add("Amount must be positive number");
        }

        if (input.Date is null)
        {
            errors.Add("Transaction date can not be empty");
        }
        else if (input.Date > DateTime.Now)
        {
            errors.Add("Transaction date can not be set in future");
        }
        
        return errors;
    }

    public IEnumerable<TransactionTypeFilter> GetTransactionTypeFilterOptions()
    {
        yield return new ("All", null);

        foreach (TransactionType type in Enum.GetValues(typeof(TransactionType)))
        {
            yield return new(type.ToString(), type);
        }
    }
}