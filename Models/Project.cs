namespace FreelanceManagementSystem.Models;

public class Project
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string ClientId { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }
    public DateTime CreatedDate { get; set; }
    public Deadline? Deadline { get; set; }

    public Project()
    {
        Id = Guid.NewGuid().ToString();
        Name = string.Empty;
        ClientId = string.Empty;
        Description = string.Empty;
        Status = "In Progress";
        CreatedDate = DateTime.Now;
        Deadline = null;
    }

    public Project(string name, string clientId, string description)
    {
        Id = Guid.NewGuid().ToString();
        Name = name;
        ClientId = clientId;
        Description = description;
        Status = "In Progress";
        CreatedDate = DateTime.Now;
        Deadline = null;
    }

    public override string ToString()
    {
        string deadlineInfo = Deadline != null ? $"Deadline: {Deadline.DueDate:yyyy-MM-dd}" : "No deadline";
        return $"[{Id[..8]}] {Name} | Client: {ClientId[..8]} | Status: {Status} | {deadlineInfo}";
    }

    public string GetDetailedInfo()
    {
        var info = $@"
╔══════════════════════════════════════════════════════════════════
║ PROJECT DETAILS
╠══════════════════════════════════════════════════════════════════
║ ID:          {Id}
║ Name:        {Name}
║ Client ID:   {ClientId}
║ Description: {Description}
║ Status:      {Status}
║ Created:     {CreatedDate:yyyy-MM-dd HH:mm}
";
        if (Deadline != null)
        {
            info += $"║ Deadline:    {Deadline.DueDate:yyyy-MM-dd} - {Deadline.Description}\n";
        }
        else
        {
            info += "║ Deadline:    Not set\n";
        }
        info += "╚══════════════════════════════════════════════════════════════════";
        return info;
    }
}
