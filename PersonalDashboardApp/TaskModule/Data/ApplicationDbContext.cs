using Microsoft.EntityFrameworkCore;
using PersonalDashboardApp.FinanceModule.Models;
using PersonalDashboardApp.TaskModule.Models;

namespace PersonalDashboardApp.TaskModule.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<TaskItem> Tasks { get; set; }
    public DbSet<FinanceRecord> FinanceRecords { get; set; }
}