using PersonalDashboardApp.TaskModule.Data;

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
        return dbContext.Tasks.ToList();
    }

    public IEnumerable<TaskItem> GetTasksByTitle(string title)
    {
        return dbContext.Tasks.Where(t => t.Title == title);
    }
}