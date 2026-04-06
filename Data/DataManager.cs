using System.Text.Json;
using FreelanceManagementSystem.Models;

namespace FreelanceManagementSystem.Data;

public class DataManager
{
    private readonly string projectsFile = "projects.json";
    private readonly string clientsFile = "clients.json";
    private readonly string invoicesFile = "invoices.json";

    public void SaveProjects(List<Project> projects)
    {
        try
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(projects, options);
            File.WriteAllText(projectsFile, jsonString);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n⚠ Error saving projects: {ex.Message}");
        }
    }

    public List<Project> LoadProjects()
    {
        try
        {
            if (File.Exists(projectsFile))
            {
                string jsonString = File.ReadAllText(projectsFile);
                return JsonSerializer.Deserialize<List<Project>>(jsonString) ?? new List<Project>();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n⚠ Error loading projects: {ex.Message}");
        }
        return new List<Project>();
    }

    public void SaveClients(List<Client> clients)
    {
        try
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(clients, options);
            File.WriteAllText(clientsFile, jsonString);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n⚠ Error saving clients: {ex.Message}");
        }
    }

    public List<Client> LoadClients()
    {
        try
        {
            if (File.Exists(clientsFile))
            {
                string jsonString = File.ReadAllText(clientsFile);
                return JsonSerializer.Deserialize<List<Client>>(jsonString) ?? new List<Client>();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n⚠ Error loading clients: {ex.Message}");
        }
        return new List<Client>();
    }

    public void SaveInvoices(List<Invoice> invoices)
    {
        try
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(invoices, options);
            File.WriteAllText(invoicesFile, jsonString);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n⚠ Error saving invoices: {ex.Message}");
        }
    }

    public List<Invoice> LoadInvoices()
    {
        try
        {
            if (File.Exists(invoicesFile))
            {
                string jsonString = File.ReadAllText(invoicesFile);
                return JsonSerializer.Deserialize<List<Invoice>>(jsonString) ?? new List<Invoice>();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n⚠ Error loading invoices: {ex.Message}");
        }
        return new List<Invoice>();
    }

    public void SaveAllData(List<Project> projects, List<Client> clients, List<Invoice> invoices)
    {
        SaveProjects(projects);
        SaveClients(clients);
        SaveInvoices(invoices);
    }
}
