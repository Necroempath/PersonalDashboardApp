using PersonalDashboardApp.FinanceModule.Models.Enums;

namespace PersonalDashboardApp.FinanceModule.Models.DTOs;

public class FinanceInputDto
{
    public TransactionType Type { get; set; }
    public string Amount { get; set; }
    public string Category { get; set; }
    public DateTime? Date { get; set; }
    public string Note { get; set; }
}