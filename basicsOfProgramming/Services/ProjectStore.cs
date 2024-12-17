using System.Collections.ObjectModel;
using System.Text.Json;
using basicsOfProgramming.Models.Projects;

namespace basicsOfProgramming.Services;

public class ProjectStore: BaseStore<Project>
{
    private static ProjectStore? _instance;
    public static ProjectStore Instance => _instance ??= new ProjectStore();
    
    protected override string FilePath { get; } = 
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "projects.json");
    public ObservableCollection<Project> Projects { get; } = new();
    
    static ProjectStore()
    {
        _instance = new ProjectStore();
        Console.WriteLine("ProjectStore static constructor called.");
    }

    private ProjectStore()
    {
        Projects.Add(new Project("Sample Project", "Description", DateTime.Now, DateTime.Now.AddDays(10), ProjectPriority.Medium));
        Projects.Add(new Project("Sample Project", "Description", DateTime.Now, DateTime.Now.AddDays(10), ProjectPriority.Medium));
        Projects.Add(new Project("Sample Project", "Description", DateTime.Now, DateTime.Now.AddDays(10), ProjectPriority.Medium));
        Projects.Add(new Project("Sample Project", "Description", DateTime.Now, DateTime.Now.AddDays(10), ProjectPriority.Medium));
    }

    public void AddProject(Project project)
    {
        Projects.Add(project);
    }
    
    public void SaveProjectsToFile()
    {
        SaveToFile(Projects);
    }
    
    public void LoadProjectsFromFile()
    {
        var loadedProjects = LoadFromFile();
        Projects.Clear();
        foreach (var project in loadedProjects)
        {
            Projects.Add(project);
        }
    }
}