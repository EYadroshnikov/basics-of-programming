using System.Collections.ObjectModel;
using System.Text.Json;
using basicsOfProgramming.Models.Projects;

namespace basicsOfProgramming.Services;

public class ProjectStore: BaseStore<Project>
{
    private static ProjectStore? _instance;
    public static ProjectStore Instance => _instance ??= new ProjectStore();
    
    public Project this[int index]
    {
        get => Projects[index];
    }

    // Индексатор для доступа к проекту по имени
    public Project this[string name]
    {
        get => Projects.FirstOrDefault(p => p.Name == name) 
               ?? throw new ArgumentException("Project not found");
    }
    
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
        foreach (var project in Projects)
        {
            SubscribeToProjectEvents(project);
        }
    }
    
    private void SubscribeToProjectEvents(Project project)
    {
        project.ProjectChanged += OnProjectChanged;
    }

    private void OnProjectChanged(object sender, ProjectEventArgs e)
    {
        Console.WriteLine($"Project event: {e.Message}, Task: {e.Task.Title}");
    }

    public void AddProject(Project project)
    {
        Projects.Add(project);
        SubscribeToProjectEvents(project);
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