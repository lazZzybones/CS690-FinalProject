using FreelanceManagementSystem.Models;

namespace FreelanceManagementSystem.Services;

public class ProjectService
{
    private List<Project> projects;

    public ProjectService()
    {
        projects = new List<Project>();
    }

    public ProjectService(List<Project> existingProjects)
    {
        projects = existingProjects ?? new List<Project>();
    }

    // FR1: Create New Project (from v1.0.0)
    public Project CreateProject(string name, string clientId, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Project name cannot be empty.");
        }

        if (string.IsNullOrWhiteSpace(clientId))
        {
            throw new ArgumentException("Client ID cannot be empty.");
        }

        var project = new Project(name, clientId, description);
        projects.Add(project);
        return project;
    }

    // FR2: View All Projects (from v1.0.0)
    public List<Project> GetAllProjects()
    {
        return projects;
    }

    // FR3: Add Deadline to Project (from v1.0.0)
    public void AddDeadlineToProject(string projectId, DateTime dueDate, string description)
    {
        var project = projects.FirstOrDefault(p => p.Id == projectId);
        if (project == null)
        {
            throw new ArgumentException("Project not found.");
        }

        if (dueDate < DateTime.Now.Date)
        {
            throw new ArgumentException("Deadline date cannot be in the past.");
        }

        var deadline = new Deadline(project.Id, dueDate, description);
        project.Deadline = deadline;
    }

    // FR9: Update Project Status (NEW in v2.0.0)
    public void UpdateProjectStatus(string projectId, string newStatus)
    {
        var project = projects.FirstOrDefault(p => p.Id == projectId);
        if (project == null)
        {
            throw new ArgumentException("Project not found.");
        }

        var validStatuses = new[] { "In Progress", "Completed", "On Hold", "Cancelled" };
        if (!validStatuses.Contains(newStatus))
        {
            throw new ArgumentException($"Invalid status. Must be one of: {string.Join(", ", validStatuses)}");
        }

        project.Status = newStatus;
    }

    public Project? GetProjectById(string id)
    {
        return projects.FirstOrDefault(p => p.Id == id);
    }

    public Project? GetProjectByIndex(int index)
    {
        if (index >= 0 && index < projects.Count)
        {
            return projects[index];
        }
        return null;
    }

    public int GetProjectCount()
    {
        return projects.Count;
    }

    public void SetProjects(List<Project> newProjects)
    {
        projects = newProjects ?? new List<Project>();
    }
}
