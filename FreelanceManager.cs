using System.Text.Json;

namespace FreelanceManagementSystem;

public class FreelanceManager
{
    private List<Project> projects;
    private readonly string dataFilePath = "projects.json";

    public FreelanceManager()
    {
        projects = new List<Project>();
        LoadData();
    }

    // FR1: Create New Project
    public void CreateProject(string name, string clientName, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Project name cannot be empty.");
        }

        if (string.IsNullOrWhiteSpace(clientName))
        {
            throw new ArgumentException("Client name cannot be empty.");
        }

        var project = new Project(name, clientName, description);
        projects.Add(project);
        SaveData();

        Console.WriteLine("\n✓ Project created successfully!");
        Console.WriteLine(project.GetDetailedInfo());
    }

    // FR2: View All Projects
    public void ViewAllProjects()
    {
        if (projects.Count == 0)
        {
            Console.WriteLine("\n⚠ No projects found. Create your first project!");
            return;
        }

        Console.WriteLine("\n╔══════════════════════════════════════════════════════════════════");
        Console.WriteLine($"║ ALL PROJECTS ({projects.Count} total)");
        Console.WriteLine("╠══════════════════════════════════════════════════════════════════");

        for (int i = 0; i < projects.Count; i++)
        {
            Console.WriteLine($"║ {i + 1}. {projects[i]}");
        }

        Console.WriteLine("╚══════════════════════════════════════════════════════════════════");
    }

    // FR3: Add Deadline to Project
    public void AddDeadlineToProject(int projectIndex, DateTime dueDate, string description)
    {
        if (projectIndex < 0 || projectIndex >= projects.Count)
        {
            throw new ArgumentException("Invalid project selection.");
        }

        if (dueDate < DateTime.Now.Date)
        {
            throw new ArgumentException("Deadline date cannot be in the past.");
        }

        var project = projects[projectIndex];
        var deadline = new Deadline(project.Id, dueDate, description);
        project.Deadline = deadline;
        SaveData();

        Console.WriteLine($"\n✓ Deadline added to project '{project.Name}'");
        Console.WriteLine($"  Due: {dueDate:yyyy-MM-dd} - {description}");
    }

    public List<Project> GetAllProjects()
    {
        return projects;
    }

    public Project? GetProject(int index)
    {
        if (index >= 0 && index < projects.Count)
        {
            return projects[index];
        }
        return null;
    }

    private void SaveData()
    {
        try
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            string jsonString = JsonSerializer.Serialize(projects, options);
            File.WriteAllText(dataFilePath, jsonString);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n⚠ Error saving data: {ex.Message}");
        }
    }

    private void LoadData()
    {
        try
        {
            if (File.Exists(dataFilePath))
            {
                string jsonString = File.ReadAllText(dataFilePath);
                projects = JsonSerializer.Deserialize<List<Project>>(jsonString) ?? new List<Project>();
                Console.WriteLine($"✓ Loaded {projects.Count} project(s) from storage.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n⚠ Error loading data: {ex.Message}");
            projects = new List<Project>();
        }
    }
}
