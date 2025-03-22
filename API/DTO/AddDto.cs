using System;

namespace API.DTO;

public class AddDto
{
    public required string TaskName { get; set; }
    public required DateOnly TaskDate { get; set; }
}
