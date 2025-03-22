using System;
using API.Data;
using API.Entitites;
using Microsoft.AspNetCore.Mvc;

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
    public ActionResult<IEnumerable<TaskRecord>> GetTasks()
    {
        var tasks = _context.Tasks.ToList();

        return tasks;
    }

}
