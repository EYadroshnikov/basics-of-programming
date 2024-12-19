using basicsOfProgramming.Models.Tasks;

public class ProjectEventArgs : EventArgs
{
    public string Message { get; }
    public PriorityTask Task { get; }

    public ProjectEventArgs(string message, PriorityTask task)
    {
        Message = message;
        Task = task;
    }
}