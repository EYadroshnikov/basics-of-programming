namespace basicsOfProgramming.Models.Tasks;

public class Task
{
    public string Title { get; set; } = "Title";
    public string? Description { get; set; }
    public TaskStatus Status { get; private set; }
    public DateTime DueDate { get; private set; }
    public string? AssignedTo { get; private set; }
    public int Priority { get; private set; }
    public List<ChangeLog> ChangeLog { get; private set; } = new();

    public void UpdateStatus(TaskStatus newStatus, string changeDescription)
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
