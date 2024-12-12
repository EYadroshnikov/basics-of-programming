using System.Collections.ObjectModel;
using System.Text.Json;
using basicsOfProgramming.Models.Projects;

namespace basicsOfProgramming.Services;

public class ProjectStore
{
    private readonly string _filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "projects.json");
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
        this.SaveProjectsToFile(_filePath);
    }
    
    public void LoadProjectsFromFile()
    {
        this.LoadProjectsFromFile(this._filePath);
    }
    
    public void SaveProjectsToFile(string filePath)
    {
        try
        {
            var json = JsonSerializer.Serialize(Projects, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            File.WriteAllText(filePath, json);
            Console.WriteLine("Проекты успешно сохранены в файл.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при сохранении проектов: {ex.Message}");
        }
    }

    public void LoadProjectsFromFile(string filePath)
    {
        try
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Файл не найден, загрузка отменена.");
                return;
            }

            var json = File.ReadAllText(filePath);
            var loadedProjects = JsonSerializer.Deserialize<List<Project>>(json);

            if (loadedProjects == null) return;
            Projects.Clear();
            foreach (var project in loadedProjects)
            {
                Projects.Add(project);
            }
            Console.WriteLine("Проекты успешно загружены из файла.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при загрузке проектов: {ex.Message}");
        }
    }
}