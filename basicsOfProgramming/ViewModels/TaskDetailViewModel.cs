using CommunityToolkit.Mvvm.ComponentModel;
using basicsOfProgramming.Models.Tasks;
using Task = basicsOfProgramming.Models.Tasks.Task;
using TaskStatus = basicsOfProgramming.Models.Tasks.TaskStatus;


namespace basicsOfProgramming.ViewModels;

public partial class TaskDetailViewModel : BaseViewModel, IQueryAttributable
{
    [ObservableProperty] private PriorityTask _task;
    public Array TaskStatuses => Enum.GetValues(typeof(TaskStatus));
    public Command SaveTaskCommand { get; }

    public TaskDetailViewModel()
    {
        Title = "Task Details";
        SaveTaskCommand = new Command(OnSaveTask);
    }

    private async void OnSaveTask()
    {
        try
        {

            await Shell.Current.GoToAsync("..");
        }
        catch (Exception exception)
        {
            Console.WriteLine($"Navigation error: {exception.Message}");
        }
    }
    
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("Task", out var value))
        {
            Task = value as PriorityTask;
        }
        else
        {
            Task = new PriorityTask("New Task", "", TaskStatus.NotStarted, DateTime.Now.AddDays(7), "somebody", 0);
        }
        
        OnPropertyChanged(nameof(Task));
    }
}