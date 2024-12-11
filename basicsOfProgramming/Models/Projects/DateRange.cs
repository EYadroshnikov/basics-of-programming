namespace basicsOfProgramming.Models.Projects;

public struct DateRange(DateTime startDate, DateTime endDate)
{
    public DateTime StartDate { get; set; } = startDate;
    public DateTime EndDate { get; set; } = endDate;
}