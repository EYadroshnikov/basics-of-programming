namespace basicsOfProgramming.Models.Tasks;

public interface ITask
{
    string Title { get; set; }
    string? Description { get; set; }
    TaskStatus Status { get; set; }
    DateTime DueDate { get; }
    void MarkAsCompleted();
}
