namespace FreelanceManagementSystem.Models;

public class Deadline
{
    public string Id { get; set; }
    public string ProjectId { get; set; }
    public DateTime DueDate { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }

    public Deadline()
    {
        Id = Guid.NewGuid().ToString();
        ProjectId = string.Empty;
        DueDate = DateTime.Now;
        Description = string.Empty;
        IsCompleted = false;
    }

    public Deadline(string projectId, DateTime dueDate, string description)
    {
        Id = Guid.NewGuid().ToString();
        ProjectId = projectId;
        DueDate = dueDate;
        Description = description;
        IsCompleted = false;
    }

    public bool IsOverdue()
    {
        return DateTime.Now > DueDate && !IsCompleted;
    }

    public int GetDaysRemaining()
    {
        return (DueDate - DateTime.Now).Days;
    }

    public string GetStatusString()
    {
        if (IsCompleted) return "[COMPLETED]";
        if (IsOverdue()) return $"[OVERDUE by {-GetDaysRemaining()} days]";
        return $"[{GetDaysRemaining()} days left]";
    }

    public override string ToString()
    {
        return $"{DueDate:yyyy-MM-dd} - {Description} {GetStatusString()}";
    }
}
