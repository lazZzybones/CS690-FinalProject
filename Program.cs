using FreelanceManagementSystem.Services;
using FreelanceManagementSystem.Data;
using FreelanceManagementSystem.UI;

namespace FreelanceManagementSystem;

class Program
{
    static void Main(string[] args)
    {
        // Initialize data manager
        var dataManager = new DataManager();

        // Load existing data
        var projects = dataManager.LoadProjects();
        var clients = dataManager.LoadClients();
        var invoices = dataManager.LoadInvoices();

        Console.WriteLine($"✓ Loaded {projects.Count} project(s), {clients.Count} client(s), {invoices.Count} invoice(s)");

        // Initialize services
        var projectService = new ProjectService(projects);
        var clientService = new ClientService(clients);
        var invoiceService = new InvoiceService(invoices);
        var deadlineService = new DeadlineService(projectService);

        // Initialize UI
        var ui = new ConsoleUI(projectService, clientService, invoiceService, deadlineService, dataManager);

        // Run application
        ui.Run();
    }
}
