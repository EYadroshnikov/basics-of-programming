using System.Collections.ObjectModel;
using basicsOfProgramming.Models.Projects;

namespace basicsOfProgramming.Services;

public class ProjectStore
{
    static ProjectStore()
    {
        _instance = new ProjectStore();
        Console.WriteLine("ProjectStore static constructor called.");
    }

    
    private static ProjectStore? _instance;
    public static ProjectStore Instance => _instance ??= new ProjectStore();

    public ObservableCollection<Project> Projects { get; } = new();

    private ProjectStore()
    {
        Projects.Add(new Project("Sample Project1", "Some description", DateTime.Now, DateTime.Now.AddDays(10), ProjectPriority.Medium, "dotnet_bot.png"));
        Projects.Add(new Project("Sample Project2", "Some description", DateTime.Now, DateTime.Now.AddDays(10), ProjectPriority.Medium, "images.jpeg"));
        Projects.Add(new Project("Sample Project3", "Some description", DateTime.Now, DateTime.Now.AddDays(10), ProjectPriority.Medium, "dotnet_bot.png"));
        Projects.Add(new Project("Sample Project4", "Some description", DateTime.Now, DateTime.Now.AddDays(10), ProjectPriority.Medium, "images.jpeg"));
    }

    public void AddProject(Project project)
    {
        Projects.Add(project);
    }
}