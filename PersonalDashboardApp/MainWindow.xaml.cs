using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PersonalDashboardApp.TaskModule.Data;
using PersonalDashboardApp.TaskModule.Models;
using PersonalDashboardApp.TaskModule.Presenters;
using PersonalDashboardApp.TaskModule.Views;

namespace PersonalDashboardApp;

public partial class MainWindow : Window
{
    private ApplicationDbContext _dbContext;
    public MainWindow()
    {
        InitializeComponent();
        var configuration = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory).AddJsonFile("appsettings.json", optional: false).Build();
        string connectionString = configuration.GetConnectionString("DefaultConnection");
        
        var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(connectionString).Options;
        _dbContext = new ApplicationDbContext(options);
        TaskPresenter presenter = new(TaskViewControl, new SqlTaskRepository(_dbContext));
        // string query = "CREATE DATABASE [TaskFinanceDb]";
        // using SqlConnection sqlConnection = new(connectionString);
        // using SqlCommand sqlCommand = new(query, sqlConnection);
        // sqlConnection.Open();
        // sqlCommand.ExecuteNonQuery();
        _dbContext.Database.EnsureCreated();
        
        var allTasks = _dbContext.Tasks.ToList();
        foreach (var task in allTasks)
        {
            TaskViewControl.Tasks.Add(task);
        }
    }
}