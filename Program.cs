namespace FreelanceManagementSystem;

class Program
{
    static void Main(string[] args)
    {
        Console.Clear();
        ShowWelcomeBanner();

        var manager = new FreelanceManager();
        bool running = true;

        while (running)
        {
            ShowMainMenu();
            string? choice = Console.ReadLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        CreateNewProject(manager);
                        break;
                    case "2":
                        ViewAllProjects(manager);
                        break;
                    case "3":
                        AddDeadline(manager);
                        break;
                    case "4":
                        running = false;
                        ShowGoodbyeMessage();
                        break;
                    default:
                        Console.WriteLine("\n⚠ Invalid option. Please try again.");
                        break;
                }

                if (running && choice != "2")
                {
                    PressAnyKeyToContinue();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n❌ Error: {ex.Message}");
                PressAnyKeyToContinue();
            }
        }
    }

    static void ShowWelcomeBanner()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(@"
╔═══════════════════════════════════════════════════════════════════════════╗
║                                                                           ║
║        FREELANCE MANAGEMENT SYSTEM v1.0.0                                ║
║        Organize your projects, deadlines, and clients                    ║
║                                                                           ║
╚═══════════════════════════════════════════════════════════════════════════╝
");
        Console.ResetColor();
    }

    static void ShowMainMenu()
    {
        Console.WriteLine("\n┌─────────────────────────────────────────┐");
        Console.WriteLine("│           MAIN MENU                     │");
        Console.WriteLine("├─────────────────────────────────────────┤");
        Console.WriteLine("│ 1. Create New Project                   │");
        Console.WriteLine("│ 2. View All Projects                    │");
        Console.WriteLine("│ 3. Add Deadline to Project              │");
        Console.WriteLine("│ 4. Exit                                 │");
        Console.WriteLine("└─────────────────────────────────────────┘");
        Console.Write("\nSelect an option: ");
    }

    static void CreateNewProject(FreelanceManager manager)
    {
        Console.WriteLine("\n┌─────────────────────────────────────────┐");
        Console.WriteLine("│      CREATE NEW PROJECT                 │");
        Console.WriteLine("└─────────────────────────────────────────┘");

        Console.Write("\nProject Name: ");
        string? name = Console.ReadLine();

        Console.Write("Client Name: ");
        string? clientName = Console.ReadLine();

        Console.Write("Description (optional): ");
        string? description = Console.ReadLine();

        manager.CreateProject(
            name ?? string.Empty,
            clientName ?? string.Empty,
            description ?? string.Empty
        );
    }

    static void ViewAllProjects(FreelanceManager manager)
    {
        manager.ViewAllProjects();

        if (manager.GetAllProjects().Count > 0)
        {
            Console.Write("\nPress Enter to view details of a project (or any other key to return): ");
            string? input = Console.ReadLine();

            if (input == "")
            {
                Console.Write("Enter project number: ");
                if (int.TryParse(Console.ReadLine(), out int projectNum) && projectNum > 0)
                {
                    var project = manager.GetProject(projectNum - 1);
                    if (project != null)
                    {
                        Console.WriteLine(project.GetDetailedInfo());
                    }
                    else
                    {
                        Console.WriteLine("\n⚠ Invalid project number.");
                    }
                }
            }
        }

        PressAnyKeyToContinue();
    }

    static void AddDeadline(FreelanceManager manager)
    {
        if (manager.GetAllProjects().Count == 0)
        {
            Console.WriteLine("\n⚠ No projects available. Create a project first!");
            return;
        }

        Console.WriteLine("\n┌─────────────────────────────────────────┐");
        Console.WriteLine("│      ADD DEADLINE TO PROJECT            │");
        Console.WriteLine("└─────────────────────────────────────────┘");

        manager.ViewAllProjects();

        Console.Write("\nSelect project number: ");
        if (!int.TryParse(Console.ReadLine(), out int projectNum) || projectNum < 1)
        {
            Console.WriteLine("\n⚠ Invalid project number.");
            return;
        }

        Console.Write("Deadline date (yyyy-MM-dd): ");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime dueDate))
        {
            Console.WriteLine("\n⚠ Invalid date format. Please use yyyy-MM-dd.");
            return;
        }

        Console.Write("Deadline description: ");
        string? description = Console.ReadLine();

        manager.AddDeadlineToProject(
            projectNum - 1,
            dueDate,
            description ?? string.Empty
        );
    }

    static void ShowGoodbyeMessage()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n╔═══════════════════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║  Thank you for using Freelance Management System!                        ║");
        Console.WriteLine("║  Your data has been saved.                                                ║");
        Console.WriteLine("╚═══════════════════════════════════════════════════════════════════════════╝\n");
        Console.ResetColor();
    }

    static void PressAnyKeyToContinue()
    {
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey(true);
        Console.Clear();
        ShowWelcomeBanner();
    }
}