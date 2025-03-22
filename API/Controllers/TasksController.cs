using API.Data;
using API.DTO;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")] // localhost:5001/api/tasks
public class TasksController : ControllerBase
{
    private readonly DataContext _context;
    public TasksController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskRecord>>> GetTasksAsync()
    {
        var tasks = await _context.Tasks.ToListAsync();

        return tasks;
    }

    [HttpPost("add")] //localhost:5001/api/tasks/add
    public async Task<ActionResult<TaskDto>> AddTask(AddDto addDto)
    {
        var task = new TaskRecord
        {
            TaskName = addDto.TaskName.ToLower(),
            TaskDate = addDto.TaskDate
        };

        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(AddTask), new
        {
            message = "Record added successfully",
            data = new TaskDto
            {
                TaskName = task.TaskName,
                TaskDate = task.TaskDate
            }
        });
    }

    [HttpDelete("delete/{id:int}")] // localhost:5001/api/tasks/delete
    public async Task<ActionResult> DeleteTask(int id)
    {
        var task = await _context.Tasks.FindAsync(id);

        if (task == null) return NotFound();

        _context.Remove(task);

        if (await _context.SaveChangesAsync() > 0)
        {
            return Ok("Record deleted successfully");
        }

        return BadRequest("Problem deleting task");
    }

}
