using TaskManagement.Application.Models;

namespace TaskManagement.Application.Services;
public class TaskService
{
    private readonly List<TaskModel> _tasks = [];
    private int _lastId = 0;

    public TaskModel Register(TaskModel task)
    {
        task.Id = ++_lastId;
        _tasks.Add(task);
        return task;
    }
    public List<TaskModel> GetAll()
    {
        return _tasks;
    }

    public TaskModel? GetById(int id)
    {
        return _tasks.FirstOrDefault(t => t.Id == id);
    }

    public bool Update(TaskModel updatedTask)
    {
        var existingTask = _tasks.FirstOrDefault(t => t.Id == updatedTask.Id);
        if (existingTask == null)
        {
            return false;
        }

        existingTask.Name = updatedTask.Name;
        existingTask.Description = updatedTask.Description;
        existingTask.Priority = updatedTask.Priority;
        existingTask.DueDate = updatedTask.DueDate;
        existingTask.Status = updatedTask.Status;

        return true;
    }

    public bool Delete(int id)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == id);
        if (task == null)
        {
            return false;
        }
        _tasks.Remove(task);
        return true;
    }
}
