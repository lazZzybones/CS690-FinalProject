using FreelanceManagementSystem.Models;
using FreelanceManagementSystem.Services;
using FreelanceManagementSystem.Data;

namespace FreelanceManagementSystem.UI;

public class ConsoleUI
{
    private readonly ProjectService projectService;
    private readonly ClientService clientService;
    private readonly InvoiceService invoiceService;
    private readonly DeadlineService deadlineService;
    private readonly DataManager dataManager;

    public ConsoleUI(ProjectService projectService, ClientService clientService, 
                     InvoiceService invoiceService, DeadlineService deadlineService, 
                     DataManager dataManager)
    {
        this.projectService = projectService;
        this.clientService = clientService;
        this.invoiceService = invoiceService;
        this.deadlineService = deadlineService;
        this.dataManager = dataManager;
    }

    public void Run()
    {
        Console.Clear();
        ShowWelcomeBanner();

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
                        CreateNewProject();
                        break;
                    case "2":
                        ViewAllProjects();
                        break;
                    case "3":
                        AddDeadline();
                        break;
                    case "4":
                        ViewUpcomingDeadlines();
                        break;
                    case "5":
                        AddNewClient();
                        break;
                    case "6":
                        ViewAllClients();
                        break;
                    case "7":
                        CreateInvoice();
                        break;
                    case "8":
                        ViewInvoiceStatus();
                        break;
                    case "9":
                        UpdateProjectStatus();
                        break;
                    case "0":
                        running = false;
                        SaveAndExit();
                        break;
                    default:
                        Console.WriteLine("\n⚠ Invalid option. Please try again.");
                        break;
                }

                if (running && choice != "2" && choice != "4" && choice != "6" && choice != "8")
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

    private void ShowWelcomeBanner()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(@"
╔═══════════════════════════════════════════════════════════════════════════╗
║                                                                           ║
║        FREELANCE MANAGEMENT SYSTEM v2.0.0                                ║
║        Organize projects, clients, invoices, and deadlines               ║
║                                                                           ║
╚═══════════════════════════════════════════════════════════════════════════╝
");
        Console.ResetColor();
    }

    private void ShowMainMenu()
    {
        Console.WriteLine("\n┌─────────────────────────────────────────┐");
        Console.WriteLine("│           MAIN MENU                     │");
        Console.WriteLine("├─────────────────────────────────────────┤");
        Console.WriteLine("│ PROJECTS                                │");
        Console.WriteLine("│ 1. Create New Project                   │");
        Console.WriteLine("│ 2. View All Projects                    │");
        Console.WriteLine("│ 3. Add Deadline to Project              │");
        Console.WriteLine("│ 4. View Upcoming Deadlines              │");
        Console.WriteLine("│ 9. Update Project Status                │");
        Console.WriteLine("├─────────────────────────────────────────┤");
        Console.WriteLine("│ CLIENTS                                 │");
        Console.WriteLine("│ 5. Add New Client                       │");
        Console.WriteLine("│ 6. View All Clients                     │");
        Console.WriteLine("├─────────────────────────────────────────┤");
        Console.WriteLine("│ INVOICES                                │");
        Console.WriteLine("│ 7. Create Invoice                       │");
        Console.WriteLine("│ 8. View Invoice Status                  │");
        Console.WriteLine("├─────────────────────────────────────────┤");
        Console.WriteLine("│ 0. Exit                                 │");
        Console.WriteLine("└─────────────────────────────────────────┘");
        Console.Write("\nSelect an option: ");
    }

    // FR1: Create New Project
    private void CreateNewProject()
    {
        Console.WriteLine("\n┌─────────────────────────────────────────┐");
        Console.WriteLine("│      CREATE NEW PROJECT                 │");
        Console.WriteLine("└─────────────────────────────────────────┘");

        if (clientService.GetClientCount() == 0)
        {
            Console.WriteLine("\n⚠ No clients found. Please add a client first!");
            return;
        }

        // Show clients
        var clients = clientService.GetAllClients();
        Console.WriteLine("\nAvailable Clients:");
        for (int i = 0; i < clients.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {clients[i].Name} ({clients[i].Company})");
        }

        Console.Write("\nSelect client number: ");
        if (!int.TryParse(Console.ReadLine(), out int clientNum) || clientNum < 1 || clientNum > clients.Count)
        {
            Console.WriteLine("\n⚠ Invalid client number.");
            return;
        }

        var selectedClient = clients[clientNum - 1];

        Console.Write("Project Name: ");
        string? name = Console.ReadLine();

        Console.Write("Description (optional): ");
        string? description = Console.ReadLine();

        var project = projectService.CreateProject(
            name ?? string.Empty,
            selectedClient.Id,
            description ?? string.Empty
        );

        dataManager.SaveProjects(projectService.GetAllProjects());

        Console.WriteLine("\n✓ Project created successfully!");
        Console.WriteLine(project.GetDetailedInfo());
    }

    // FR2: View All Projects
    private void ViewAllProjects()
    {
        var projects = projectService.GetAllProjects();

        if (projects.Count == 0)
        {
            Console.WriteLine("\n⚠ No projects found. Create your first project!");
            PressAnyKeyToContinue();
            return;
        }

        Console.WriteLine("\n╔══════════════════════════════════════════════════════════════════");
        Console.WriteLine($"║ ALL PROJECTS ({projects.Count} total)");
        Console.WriteLine("╠══════════════════════════════════════════════════════════════════");

        for (int i = 0; i < projects.Count; i++)
        {
            var client = clientService.GetClientById(projects[i].ClientId);
            string clientName = client != null ? client.Name : "Unknown Client";
            Console.WriteLine($"║ {i + 1}. [{projects[i].Id[..8]}] {projects[i].Name} | Client: {clientName} | Status: {projects[i].Status}");
        }

        Console.WriteLine("╚══════════════════════════════════════════════════════════════════");
        PressAnyKeyToContinue();
    }

    // FR3: Add Deadline
    private void AddDeadline()
    {
        if (projectService.GetProjectCount() == 0)
        {
            Console.WriteLine("\n⚠ No projects available. Create a project first!");
            return;
        }

        Console.WriteLine("\n┌─────────────────────────────────────────┐");
        Console.WriteLine("│      ADD DEADLINE TO PROJECT            │");
        Console.WriteLine("└─────────────────────────────────────────┘");

        var projects = projectService.GetAllProjects();
        for (int i = 0; i < projects.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {projects[i].Name}");
        }

        Console.Write("\nSelect project number: ");
        if (!int.TryParse(Console.ReadLine(), out int projectNum) || projectNum < 1)
        {
            Console.WriteLine("\n⚠ Invalid project number.");
            return;
        }

        var project = projectService.GetProjectByIndex(projectNum - 1);
        if (project == null)
        {
            Console.WriteLine("\n⚠ Project not found.");
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

        projectService.AddDeadlineToProject(project.Id, dueDate, description ?? string.Empty);
        dataManager.SaveProjects(projectService.GetAllProjects());

        Console.WriteLine($"\n✓ Deadline added to project '{project.Name}'");
        Console.WriteLine($"  Due: {dueDate:yyyy-MM-dd} - {description}");
    }

    // FR4: View Upcoming Deadlines
    private void ViewUpcomingDeadlines()
    {
        var deadlines = deadlineService.GetUpcomingDeadlines();

        if (deadlines.Count == 0)
        {
            Console.WriteLine("\n⚠ No deadlines found.");
            PressAnyKeyToContinue();
            return;
        }

        Console.WriteLine("\n╔══════════════════════════════════════════════════════════════════");
        Console.WriteLine($"║ UPCOMING DEADLINES ({deadlines.Count} total)");
        Console.WriteLine("╠══════════════════════════════════════════════════════════════════");

        foreach (var (project, deadline) in deadlines)
        {
            var statusColor = deadline.IsOverdue() ? ConsoleColor.Red : ConsoleColor.Green;
            Console.ForegroundColor = statusColor;
            Console.Write($"║ {deadline.DueDate:yyyy-MM-dd} ");
            Console.ResetColor();
            Console.WriteLine($"| {project.Name} | {deadline.Description} {deadline.GetStatusString()}");
        }

        Console.WriteLine("╚══════════════════════════════════════════════════════════════════");
        PressAnyKeyToContinue();
    }

    // FR5: Create Invoice
    private void CreateInvoice()
    {
        Console.WriteLine("\n┌─────────────────────────────────────────┐");
        Console.WriteLine("│      CREATE INVOICE                     │");
        Console.WriteLine("└─────────────────────────────────────────┘");

        if (projectService.GetProjectCount() == 0)
        {
            Console.WriteLine("\n⚠ No projects available. Create a project first!");
            return;
        }

        var projects = projectService.GetAllProjects();
        for (int i = 0; i < projects.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {projects[i].Name}");
        }

        Console.Write("\nSelect project number: ");
        if (!int.TryParse(Console.ReadLine(), out int projectNum) || projectNum < 1)
        {
            Console.WriteLine("\n⚠ Invalid project number.");
            return;
        }

        var project = projectService.GetProjectByIndex(projectNum - 1);
        if (project == null)
        {
            Console.WriteLine("\n⚠ Project not found.");
            return;
        }

        Console.Write("Invoice amount: $");
        if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
        {
            Console.WriteLine("\n⚠ Invalid amount.");
            return;
        }

        Console.Write("Due date (yyyy-MM-dd): ");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime dueDate))
        {
            Console.WriteLine("\n⚠ Invalid date format.");
            return;
        }

        var invoice = invoiceService.CreateInvoice(project.Id, project.ClientId, amount, dueDate);
        dataManager.SaveInvoices(invoiceService.GetAllInvoices());

        Console.WriteLine("\n✓ Invoice created successfully!");
        Console.WriteLine(invoice.GetDetailedInfo());
    }

    // FR6: View Invoice Status
    private void ViewInvoiceStatus()
    {
        var invoices = invoiceService.GetAllInvoices();

        if (invoices.Count == 0)
        {
            Console.WriteLine("\n⚠ No invoices found.");
            PressAnyKeyToContinue();
            return;
        }

        Console.WriteLine("\n╔══════════════════════════════════════════════════════════════════");
        Console.WriteLine($"║ ALL INVOICES ({invoices.Count} total)");
        Console.WriteLine("╠══════════════════════════════════════════════════════════════════");

        decimal totalPaid = 0;
        decimal totalUnpaid = 0;

        for (int i = 0; i < invoices.Count; i++)
        {
            var invoice = invoices[i];
            if (invoice.IsPaid)
                totalPaid += invoice.Amount;
            else
                totalUnpaid += invoice.Amount;

            var statusColor = invoice.IsPaid ? ConsoleColor.Green : 
                             invoice.IsOverdue() ? ConsoleColor.Red : ConsoleColor.Yellow;
            
            Console.Write($"║ {i + 1}. ");
            Console.ForegroundColor = statusColor;
            Console.Write($"[{invoice.InvoiceNumber}]");
            Console.ResetColor();
            Console.WriteLine($" ${invoice.Amount:F2} | Due: {invoice.DueDate:yyyy-MM-dd} | {invoice.GetStatus()}");
        }

        Console.WriteLine("╠══════════════════════════════════════════════════════════════════");
        Console.WriteLine($"║ PAID: ${totalPaid:F2} | UNPAID: ${totalUnpaid:F2}");
        Console.WriteLine("╚══════════════════════════════════════════════════════════════════");
        PressAnyKeyToContinue();
    }

    // FR7: Add New Client
    private void AddNewClient()
    {
        Console.WriteLine("\n┌─────────────────────────────────────────┐");
        Console.WriteLine("│      ADD NEW CLIENT                     │");
        Console.WriteLine("└─────────────────────────────────────────┘");

        Console.Write("\nClient Name: ");
        string? name = Console.ReadLine();

        Console.Write("Email: ");
        string? email = Console.ReadLine();

        Console.Write("Phone: ");
        string? phone = Console.ReadLine();

        Console.Write("Company: ");
        string? company = Console.ReadLine();

        var client = clientService.AddClient(
            name ?? string.Empty,
            email ?? string.Empty,
            phone ?? string.Empty,
            company ?? string.Empty
        );

        dataManager.SaveClients(clientService.GetAllClients());

        Console.WriteLine("\n✓ Client added successfully!");
        Console.WriteLine(client.GetDetailedInfo());
    }

    // FR8: View All Clients
    private void ViewAllClients()
    {
        var clients = clientService.GetAllClients();

        if (clients.Count == 0)
        {
            Console.WriteLine("\n⚠ No clients found. Add your first client!");
            PressAnyKeyToContinue();
            return;
        }

        Console.WriteLine("\n╔══════════════════════════════════════════════════════════════════");
        Console.WriteLine($"║ ALL CLIENTS ({clients.Count} total)");
        Console.WriteLine("╠══════════════════════════════════════════════════════════════════");

        for (int i = 0; i < clients.Count; i++)
        {
            Console.WriteLine($"║ {i + 1}. {clients[i]}");
        }

        Console.WriteLine("╚══════════════════════════════════════════════════════════════════");
        PressAnyKeyToContinue();
    }

    // FR9: Update Project Status
    private void UpdateProjectStatus()
    {
        if (projectService.GetProjectCount() == 0)
        {
            Console.WriteLine("\n⚠ No projects available.");
            return;
        }

        Console.WriteLine("\n┌─────────────────────────────────────────┐");
        Console.WriteLine("│      UPDATE PROJECT STATUS              │");
        Console.WriteLine("└─────────────────────────────────────────┘");

        var projects = projectService.GetAllProjects();
        for (int i = 0; i < projects.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {projects[i].Name} (Current: {projects[i].Status})");
        }

        Console.Write("\nSelect project number: ");
        if (!int.TryParse(Console.ReadLine(), out int projectNum) || projectNum < 1)
        {
            Console.WriteLine("\n⚠ Invalid project number.");
            return;
        }

        var project = projectService.GetProjectByIndex(projectNum - 1);
        if (project == null)
        {
            Console.WriteLine("\n⚠ Project not found.");
            return;
        }

        Console.WriteLine("\nAvailable statuses:");
        Console.WriteLine("1. In Progress");
        Console.WriteLine("2. Completed");
        Console.WriteLine("3. On Hold");
        Console.WriteLine("4. Cancelled");

        Console.Write("\nSelect new status: ");
        string? statusChoice = Console.ReadLine();

        string newStatus = statusChoice switch
        {
            "1" => "In Progress",
            "2" => "Completed",
            "3" => "On Hold",
            "4" => "Cancelled",
            _ => throw new ArgumentException("Invalid status choice")
        };

        projectService.UpdateProjectStatus(project.Id, newStatus);
        dataManager.SaveProjects(projectService.GetAllProjects());

        Console.WriteLine($"\n✓ Project status updated to: {newStatus}");
    }

    private void SaveAndExit()
    {
        dataManager.SaveAllData(
            projectService.GetAllProjects(),
            clientService.GetAllClients(),
            invoiceService.GetAllInvoices()
        );

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n╔═══════════════════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║  Thank you for using Freelance Management System!                        ║");
        Console.WriteLine("║  All data has been saved.                                                 ║");
        Console.WriteLine("╚═══════════════════════════════════════════════════════════════════════════╝\n");
        Console.ResetColor();
    }

    private void PressAnyKeyToContinue()
    {
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey(true);
        Console.Clear();
        ShowWelcomeBanner();
    }
}
