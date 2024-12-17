namespace basicsOfProgramming.Models.Tasks;

public class PriorityTask : Task
{
    private string _title;
    public override string Title
    {
        get => _title;
        set => _title = $"{value} (Priority: {Priority})";
    }
    public int Priority { get; private set; }

    public PriorityTask(string title, string? description, TaskStatus status, DateTime dueDate, string? assignedTo,
        int priority)
        : base(title, description, status, dueDate, assignedTo)
    {
        Priority = priority;
        _title = title;
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