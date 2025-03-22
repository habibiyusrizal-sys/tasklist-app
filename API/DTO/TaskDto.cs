using System;

namespace API.DTO;

public class TaskDto
{
    public required string TaskName { get; set; }
    public required DateOnly TaskDate { get; set; }
}
