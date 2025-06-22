using System.ComponentModel.DataAnnotations;
using PersonalDashboardApp.FinanceModule.Models.Enums;

namespace PersonalDashboardApp.FinanceModule.Models;

public class FinanceRecord
{
    [Key]
    public int Id { get; set; }

    public required TransactionType TransactionType { get; set; }
    
    public required decimal Amount { get; set; }
    
    public required string Category { get; set; }

    public required DateTime TransactionDate { get; set; }

    [MaxLength(100)]
    public string Note { get; set; }
}