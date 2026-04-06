using FreelanceManagementSystem.Models;

namespace FreelanceManagementSystem.Services;

public class ClientService
{
    private List<Client> clients;

    public ClientService()
    {
        clients = new List<Client>();
    }

    public ClientService(List<Client> existingClients)
    {
        clients = existingClients ?? new List<Client>();
    }

    // FR7: Add New Client
    public Client AddClient(string name, string email, string phone, string company)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Client name cannot be empty.");
        }

        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException("Client email cannot be empty.");
        }

        var client = new Client(name, email, phone, company);
        clients.Add(client);
        return client;
    }

    // FR8: View All Clients
    public List<Client> GetAllClients()
    {
        return clients;
    }

    public Client? GetClientById(string id)
    {
        return clients.FirstOrDefault(c => c.Id == id);
    }

    public Client? GetClientByIndex(int index)
    {
        if (index >= 0 && index < clients.Count)
        {
            return clients[index];
        }
        return null;
    }

    public int GetClientCount()
    {
        return clients.Count;
    }

    public void SetClients(List<Client> newClients)
    {
        clients = newClients ?? new List<Client>();
    }
}
