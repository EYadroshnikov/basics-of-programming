namespace basicsOfProgramming.Models.Projects;

public class Project
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public ProjectPriority Priority { get; set; }
    private List<Task> Tasks { get; set; } = new();

    public void AddTask(Task task) => Tasks.Add(task);
    public void RemoveTask(Task task) => Tasks.Remove(task);

    public Project(string name, string description, DateTime startDate, DateTime endDate)
    {
        Name = name;
        Description = description;
        StartDate = startDate;
        EndDate = endDate;
        Priority = 0;
    }
    public Project(string name, string description, DateTime startDate, DateTime endDate, ProjectPriority priority)
    {
        Name = name;
        Description = description;
        StartDate = startDate;
        EndDate = endDate;
        Priority = priority;
    }
}