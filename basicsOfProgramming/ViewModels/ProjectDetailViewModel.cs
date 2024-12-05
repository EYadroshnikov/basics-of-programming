using basicsOfProgramming.Models.Projects;
using basicsOfProgramming.Models.Tasks;
using basicsOfProgramming.Services;
using CommunityToolkit.Mvvm.ComponentModel;
// using Task = basicsOfProgramming.Models.Tasks.Task;
// using TaskStatus = basicsOfProgramming.Models.Tasks.TaskStatus;

namespace basicsOfProgramming.ViewModels;

public partial class ProjectDetailViewModel : BaseViewModel, IQueryAttributable
{
    [ObservableProperty] private Project _project;
    public string ProjectHash => Project?.GetHashCode().ToString() ?? "No Project";
    public Array ProjectPriorities => Enum.GetValues(typeof(ProjectPriority));
    public Command SaveProjectCommand { get; }
    public Command AddTaskCommand { get; }

    public ProjectDetailViewModel()
    {
        SaveProjectCommand = new Command(OnSaveProject);
        AddTaskCommand = new Command(OnAddTask);
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("Project", out var value))
        {
            Project = value as Project;
        }
        else
        {
            Project = new Project("New Project", "", DateTime.Now, DateTime.Now.AddDays(7));
        }
        
        OnPropertyChanged(nameof(ProjectHash));
    }

    private async void OnSaveProject()
    {
        // Логика сохранения проекта
        if (Project != null && !ProjectStore.Instance.Projects.Contains(Project))
        {
            ProjectStore.Instance.AddProject(Project);
        }

        // Возврат на предыдущую страницу
        await Shell.Current.GoToAsync("..");
    }

    private void OnAddTask()
    {
        Project?.AddTask(new Models.Tasks.Task("New Task", "", Models.Tasks.TaskStatus.NotStarted, DateTime.Now, null, 0));
    }
}