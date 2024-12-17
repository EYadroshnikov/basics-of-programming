using System.Text.Json.Serialization;
using basicsOfProgramming.Models.Tasks;
using Task = basicsOfProgramming.Models.Tasks.Task;

namespace basicsOfProgramming.Models.Projects;

public class Project
{
    private string? _name;

    public string? Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Project name can't be empty");
            }

            _name = char.ToUpper(value[0]) + value[1..];
        }
    }

    public string? Description { get; set; }
    public DateRange DateRange { get; set; }
    public ProjectPriority Priority { get; set; }

    public List<Task> Tasks { get; set; } = new List<Task>();

    public void AddTask(Task task) => Tasks.Add(task);
    public void RemoveTask(Task task) => Tasks.Remove(task);

    public Project(string name, string description, DateTime startDate, DateTime endDate)
    {
        Name = name;
        Description = description;
        DateRange = new DateRange(startDate, endDate);
        Priority = ProjectPriority.High;
    }

    public Project(string name, string description, DateTime startDate, DateTime endDate, ProjectPriority priority)
    {
        Name = name;
        Description = description;
        DateRange = new DateRange(startDate, endDate);
        Priority = priority;
    }
    
    [JsonConstructor]
    public Project(string name, string description, DateRange dateRange, ProjectPriority priority, List<Task> tasks)
    {
        Name = name;
        Description = description;
        DateRange = dateRange;
        Priority = priority;
        Tasks = tasks;
    }
    
    public int GetProjectAge()
    {
        return (int)((DateTime.Now - DateRange.StartDate).TotalDays / 365);
    }

    public int GetProjectDuration()
    {
        return (int)(DateRange.EndDate - DateRange.StartDate).TotalDays;
    }

    public int GetTimeRemaining()
    {
        return (int)(DateRange.EndDate - DateTime.Now).TotalDays;
    }
    
    public override string ToString()
    {
        return $"Project Name: {Name}\n" +
               $"Description: {Description}\n" +
               $"Start Date: {DateRange.StartDate.ToShortDateString()}\n" +
               $"End Date: {DateRange.EndDate.ToShortDateString()}\n" +
               $"Priority: {Priority}\n" +
               $"Task Count: {Tasks.Count}";
    }
}