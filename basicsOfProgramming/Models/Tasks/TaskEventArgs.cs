namespace basicsOfProgramming.Models.Tasks;

public class TaskEventArgs(string taskTitle, TaskStatus oldStatus, TaskStatus newStatus)
    : EventArgs
{
    public string TaskTitle { get; } = taskTitle;
    public TaskStatus OldStatus { get; } = oldStatus;
    public TaskStatus NewStatus { get; } = newStatus;
}