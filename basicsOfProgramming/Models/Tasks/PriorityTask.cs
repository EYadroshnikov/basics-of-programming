namespace basicsOfProgramming.Models.Tasks;

public class PriorityTask : Task
{
    public int Priority { get; private set; }

    public PriorityTask(string title, string? description, TaskStatus status, DateTime dueDate, string? assignedTo,
        int priority)
        : base(title, description, status, dueDate, assignedTo)
    {
        Priority = priority;
    }

    public void SetPriority(int newPriority)
    {
        Priority = newPriority;
    }

    public sealed override void ShowDetails()
    {
        base.ShowDetails();
        Console.WriteLine($"Priority: {Priority}");
    }
}