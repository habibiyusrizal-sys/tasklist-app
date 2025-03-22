using System;

namespace API.Entitites;

public class TaskRecord
{
    public int Id { get; set; }
    public required string TaskName { get; set; }
    public required DateOnly TaskDate { get; set; }
}
