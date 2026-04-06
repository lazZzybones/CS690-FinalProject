using FreelanceManagementSystem.Services;
using FreelanceManagementSystem.Models;
using Xunit;

namespace FreelanceManagementSystem.Tests;

public class ClientServiceTests
{
    [Fact]
    public void AddClient_ValidData_ShouldCreateClient()
    {
        // Arrange
        var service = new ClientService();

        // Act
        var client = service.AddClient("John Doe", "john@example.com", "123-456-7890", "Acme Corp");

        // Assert
        Assert.NotNull(client);
        Assert.Equal("John Doe", client.Name);
        Assert.Equal("john@example.com", client.Email);
        Assert.Equal("123-456-7890", client.Phone);
        Assert.Equal("Acme Corp", client.Company);
        Assert.NotEmpty(client.Id);
    }

    [Fact]
    public void AddClient_EmptyName_ShouldThrowException()
    {
        // Arrange
        var service = new ClientService();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => 
            service.AddClient("", "john@example.com", "123-456-7890", "Acme Corp"));
    }

    [Fact]
    public void AddClient_EmptyEmail_ShouldThrowException()
    {
        // Arrange
        var service = new ClientService();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => 
            service.AddClient("John Doe", "", "123-456-7890", "Acme Corp"));
    }

    [Fact]
    public void GetAllClients_ShouldReturnAllClients()
    {
        // Arrange
        var service = new ClientService();
        service.AddClient("Client 1", "email1@test.com", "111", "Company1");
        service.AddClient("Client 2", "email2@test.com", "222", "Company2");

        // Act
        var clients = service.GetAllClients();

        // Assert
        Assert.Equal(2, clients.Count);
    }

    [Fact]
    public void GetClientById_ExistingClient_ShouldReturnClient()
    {
        // Arrange
        var service = new ClientService();
        var client = service.AddClient("John Doe", "john@example.com", "123", "Acme");

        // Act
        var result = service.GetClientById(client.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(client.Id, result.Id);
    }

    [Fact]
    public void GetClientById_NonExistingClient_ShouldReturnNull()
    {
        // Arrange
        var service = new ClientService();

        // Act
        var result = service.GetClientById("non-existing-id");

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void GetClientCount_ShouldReturnCorrectCount()
    {
        // Arrange
        var service = new ClientService();
        service.AddClient("Client 1", "email1@test.com", "111", "Company1");
        service.AddClient("Client 2", "email2@test.com", "222", "Company2");
        service.AddClient("Client 3", "email3@test.com", "333", "Company3");

        // Act
        var count = service.GetClientCount();

        // Assert
        Assert.Equal(3, count);
    }
}
