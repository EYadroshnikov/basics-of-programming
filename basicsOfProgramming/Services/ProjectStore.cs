using System.Collections.ObjectModel;
using basicsOfProgramming.Models.Projects;

namespace basicsOfProgramming.Services;

public class ProjectStore
{
    // Singleton instance
    private static ProjectStore? _instance;
    public static ProjectStore Instance => _instance ??= new ProjectStore();

    // ObservableCollection для хранения проектов
    public ObservableCollection<Project> Projects { get; } = new();

    private ProjectStore()
    {
        // Добавьте тестовые данные, если нужно
        Projects.Add(new Project("Sample Project", "Description", DateTime.Now, DateTime.Now.AddDays(10), ProjectPriority.Medium));
    }

    // Метод добавления нового проекта
    public void AddProject(Project project)
    {
        Projects.Add(project);
    }
}