using PersonalDashboardApp.TaskModule.Data;
using PersonalDashboardApp.TaskModule.DTOs;

namespace PersonalDashboardApp.TaskModule.Models;

public class SqlTaskRepository(ApplicationDbContext dbContext) : ITaskRepository
{
    public void AddTask(TaskItem task)
    {
        dbContext.Tasks.Add(task);
        dbContext.SaveChanges();
    }

    public void UpdateTask(TaskItem task)
    {
        dbContext.Tasks.Update(task);
        dbContext.SaveChanges();
    }

    public void DeleteTask(int id)
    {
        dbContext.Tasks.Remove(dbContext.Tasks.Find(id));
        dbContext.SaveChanges();
    }

    public IEnumerable<TaskItem> GetAllTasks()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<TaskItem> GetTasksByTitle(string title)
    {
        throw new NotImplementedException();
    }
}