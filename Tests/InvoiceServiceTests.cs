using FreelanceManagementSystem.Services;
using FreelanceManagementSystem.Models;
using Xunit;

namespace FreelanceManagementSystem.Tests;

public class InvoiceServiceTests
{
    [Fact]
    public void CreateInvoice_ValidData_ShouldCreateInvoice()
    {
        // Arrange
        var service = new InvoiceService();
        var futureDate = DateTime.Now.AddDays(30);

        // Act
        var invoice = service.CreateInvoice("project-123", "client-123", 1500.00m, futureDate);

        // Assert
        Assert.NotNull(invoice);
        Assert.Equal("project-123", invoice.ProjectId);
        Assert.Equal("client-123", invoice.ClientId);
        Assert.Equal(1500.00m, invoice.Amount);
        Assert.Equal(futureDate.Date, invoice.DueDate.Date);
        Assert.False(invoice.IsPaid);
        Assert.NotEmpty(invoice.InvoiceNumber);
    }

    [Fact]
    public void CreateInvoice_ZeroAmount_ShouldThrowException()
    {
        // Arrange
        var service = new InvoiceService();
        var futureDate = DateTime.Now.AddDays(30);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => 
            service.CreateInvoice("project-123", "client-123", 0, futureDate));
    }

    [Fact]
    public void CreateInvoice_NegativeAmount_ShouldThrowException()
    {
        // Arrange
        var service = new InvoiceService();
        var futureDate = DateTime.Now.AddDays(30);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => 
            service.CreateInvoice("project-123", "client-123", -100, futureDate));
    }

    [Fact]
    public void CreateInvoice_PastDueDate_ShouldThrowException()
    {
        // Arrange
        var service = new InvoiceService();
        var pastDate = DateTime.Now.AddDays(-30);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => 
            service.CreateInvoice("project-123", "client-123", 1000, pastDate));
    }

    [Fact]
    public void GetAllInvoices_ShouldReturnAllInvoices()
    {
        // Arrange
        var service = new InvoiceService();
        var futureDate = DateTime.Now.AddDays(30);
        service.CreateInvoice("project-1", "client-1", 1000, futureDate);
        service.CreateInvoice("project-2", "client-2", 2000, futureDate);

        // Act
        var invoices = service.GetAllInvoices();

        // Assert
        Assert.Equal(2, invoices.Count);
    }

    [Fact]
    public void GetUnpaidInvoices_ShouldReturnOnlyUnpaid()
    {
        // Arrange
        var service = new InvoiceService();
        var futureDate = DateTime.Now.AddDays(30);
        var invoice1 = service.CreateInvoice("project-1", "client-1", 1000, futureDate);
        var invoice2 = service.CreateInvoice("project-2", "client-2", 2000, futureDate);
        service.MarkAsPaid(invoice1.Id);

        // Act
        var unpaidInvoices = service.GetUnpaidInvoices();

        // Assert
        Assert.Single(unpaidInvoices);
        Assert.Equal(invoice2.Id, unpaidInvoices[0].Id);
    }

    [Fact]
    public void MarkAsPaid_ShouldUpdateInvoiceStatus()
    {
        // Arrange
        var service = new InvoiceService();
        var futureDate = DateTime.Now.AddDays(30);
        var invoice = service.CreateInvoice("project-1", "client-1", 1000, futureDate);

        // Act
        service.MarkAsPaid(invoice.Id);

        // Assert
        var updatedInvoice = service.GetInvoiceById(invoice.Id);
        Assert.True(updatedInvoice.IsPaid);
    }

    [Fact]
    public void GetTotalRevenue_ShouldReturnSumOfPaidInvoices()
    {
        // Arrange
        var service = new InvoiceService();
        var futureDate = DateTime.Now.AddDays(30);
        var invoice1 = service.CreateInvoice("project-1", "client-1", 1000, futureDate);
        var invoice2 = service.CreateInvoice("project-2", "client-2", 2000, futureDate);
        var invoice3 = service.CreateInvoice("project-3", "client-3", 500, futureDate);
        
        service.MarkAsPaid(invoice1.Id);
        service.MarkAsPaid(invoice2.Id);

        // Act
        var revenue = service.GetTotalRevenue();

        // Assert
        Assert.Equal(3000m, revenue);
    }

    [Fact]
    public void GetOutstandingAmount_ShouldReturnSumOfUnpaidInvoices()
    {
        // Arrange
        var service = new InvoiceService();
        var futureDate = DateTime.Now.AddDays(30);
        var invoice1 = service.CreateInvoice("project-1", "client-1", 1000, futureDate);
        var invoice2 = service.CreateInvoice("project-2", "client-2", 2000, futureDate);
        var invoice3 = service.CreateInvoice("project-3", "client-3", 500, futureDate);
        
        service.MarkAsPaid(invoice1.Id);

        // Act
        var outstanding = service.GetOutstandingAmount();

        // Assert
        Assert.Equal(2500m, outstanding);
    }
}
