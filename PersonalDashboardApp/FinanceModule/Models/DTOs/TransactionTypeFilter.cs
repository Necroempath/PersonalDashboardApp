using PersonalDashboardApp.FinanceModule.Models.Enums;

namespace PersonalDashboardApp.FinanceModule.Models.DTOs;

public record TransactionTypeFilter(string Name, TransactionType? Value);