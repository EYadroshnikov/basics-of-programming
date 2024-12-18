using System.Collections.ObjectModel;
using basicsOfProgramming.Models.Projects;
using basicsOfProgramming.Models.Tasks;
using basicsOfProgramming.Services;
using basicsOfProgramming.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using Task = basicsOfProgramming.Models.Tasks.Task;
using TaskStatus = basicsOfProgramming.Models.Tasks.TaskStatus;

namespace basicsOfProgramming.ViewModels;

public partial class ProjectDetailViewModel : BaseViewModel, IQueryAttributable
{
    private bool _isNavigating = false;
    [ObservableProperty] private Project _project;

    public string ProjectHash => Project?.GetHashCode().ToString() ?? "No Project";
    public Array ProjectPriorities => Enum.GetValues(typeof(ProjectPriority));
    public Command SaveProjectCommand { get; }
    public Command AddTaskCommand { get; }
    private Command<PriorityTask> SelectTaskCommand { get; }

    public ObservableCollection<PriorityTask> Tasks => Project?.Tasks != null
        ? new ObservableCollection<PriorityTask>(Project.Tasks)
        : new ObservableCollection<PriorityTask>();

    [ObservableProperty] private PriorityTask? _selectedTask;

    partial void OnSelectedTaskChanged(PriorityTask value)
    {
        if (value != null && !_isNavigating)
        {
            _isNavigating = true;
            SelectTaskCommand.Execute(value);
        }
    }

    public ProjectDetailViewModel()
    {
        Title = "Project Details";
        SaveProjectCommand = new Command(OnSaveProject);
        AddTaskCommand = new Command(OnAddTask);
        SelectTaskCommand = new Command<PriorityTask>(OnSelectTask);
    }

    private async void OnSelectTask(PriorityTask task)
    {
        var navParam = new Dictionary<string, object> { { "Task", task } };
        try
        {
            await Shell.Current.GoToAsync(nameof(TaskDetailPage), navParam);
            SelectedTask = null;
            _isNavigating = false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Navigation error: {ex.Message}");
        }
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("Project", out var value))
        {
            Project = value as Project;
            OnPropertyChanged(nameof(Tasks));
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

    private async void OnAddTask()
    {
        if (Project == null)
        {
            Console.WriteLine("Error: Project is null. Cannot add a task.");
            return;
        }

        var task = new PriorityTask(
            title: "New Task",
            description: "Description here",
            status: TaskStatus.NotStarted,
            dueDate: DateTime.Now.AddDays(7),
            assignedTo: "Unassigned",
            priority: 1);

        Project.Tasks.Add(task);

        OnPropertyChanged(nameof(Tasks));

        var navParam = new Dictionary<string, object> { { "Task", task } };
        try
        {
            await Shell.Current.GoToAsync(nameof(TaskDetailPage), navParam);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Navigation error: {ex.Message}");
        }
    }
}