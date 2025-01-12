using Microsoft.AspNetCore.Mvc;
using TaskApi.Models;
using System.Collections.Generic;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private static List<TaskItem> Tasks = new List<TaskItem>
    {
        new TaskItem { Id = 1, Title = "Learn Vue.js", IsCompleted = false },
        new TaskItem { Id = 2, Title = "Build an App", IsCompleted = false }
    };

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(Tasks);
    }

    [HttpPost]
    public IActionResult AddTask([FromBody] TaskItem task)
    {
        task.Id = Tasks.Count + 1;
        Tasks.Add(task);
        return Ok(task);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateTask(int id, [FromBody] TaskItem task)
    {
        var existingTask = Tasks.Find(t => t.Id == id);
        if (existingTask == null) return NotFound();
        existingTask.Title = task.Title;
        existingTask.IsCompleted = task.IsCompleted;
        return Ok(existingTask);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteTask(int id)
    {
        var task = Tasks.Find(t => t.Id == id);
        if (task == null) return NotFound();
        Tasks.Remove(task);
        return NoContent();
    }
}
