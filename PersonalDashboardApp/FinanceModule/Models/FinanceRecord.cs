using System.ComponentModel.DataAnnotations;
using PersonalDashboardApp.FinanceModule.Models.Enums;

namespace PersonalDashboardApp.FinanceModule.Models;

public class FinanceRecord
{
    [Key]
    public int Id { get; set; }

    public required TransactionType TranasctionType { get; set; }
    
    public required float Amount { get; set; }
    
    public required Category Category { get; set; }

    public required DateTime TransactionDate { get; set; }

    public string Note { get; set; }
}