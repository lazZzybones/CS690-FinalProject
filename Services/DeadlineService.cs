using FreelanceManagementSystem.Models;

namespace FreelanceManagementSystem.Services;

public class DeadlineService
{
    private readonly ProjectService projectService;

    public DeadlineService(ProjectService projectService)
    {
        this.projectService = projectService ?? throw new ArgumentNullException(nameof(projectService));
    }

    // FR4: View Upcoming Deadlines (NEW in v2.0.0)
    public List<(Project project, Deadline deadline)> GetUpcomingDeadlines()
    {
        var result = new List<(Project, Deadline)>();

        foreach (var project in projectService.GetAllProjects())
        {
            if (project.Deadline != null)
            {
                result.Add((project, project.Deadline));
            }
        }

        // Sort by due date (ascending)
        return result.OrderBy(x => x.Item2.DueDate).ToList();
    }

    public List<(Project project, Deadline deadline)> GetOverdueDeadlines()
    {
        return GetUpcomingDeadlines()
            .Where(x => x.Item2.IsOverdue())
            .ToList();
    }

    public List<(Project project, Deadline deadline)> GetTodayDeadlines()
    {
        return GetUpcomingDeadlines()
            .Where(x => x.Item2.DueDate.Date == DateTime.Now.Date)
            .ToList();
    }

    public List<(Project project, Deadline deadline)> GetThisWeekDeadlines()
    {
        var endOfWeek = DateTime.Now.AddDays(7);
        return GetUpcomingDeadlines()
            .Where(x => x.Item2.DueDate.Date <= endOfWeek.Date && x.Item2.DueDate.Date >= DateTime.Now.Date)
            .ToList();
    }

    public int GetDeadlineCount()
    {
        return projectService.GetAllProjects().Count(p => p.Deadline != null);
    }
}
