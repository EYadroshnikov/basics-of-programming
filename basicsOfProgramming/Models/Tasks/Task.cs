namespace basicsOfProgramming.Models.Tasks;

public class Task
{
    public string Title { get; set; } = "Title";
    public string? Description { get; set; }
    public TaskStatus Status { get; private set; }
    public DateTime DueDate { get; private set; }
    public string? AssignedTo { get; private set; }
    public List<ChangeLog> ChangeLog { get; private set; } = new();

    public Task(string title, string? description, TaskStatus status, DateTime dueDate, string? assignedTo)
    {
        Title = title;
        Description = description;
        Status = status;
        DueDate = dueDate;
        AssignedTo = assignedTo;
    }

    private void UpdateStatus(TaskStatus newStatus, string changeDescription)
    {
        if (Status == newStatus) return;
        ChangeLog.Add(new ChangeLog
        {
            ChangeDescription = changeDescription
        });

        Status = newStatus;
    }

    public void MarkAsCompleted() => UpdateStatus(TaskStatus.Completed, "Task marked as completed");
}
