using FreelanceManagementSystem.Models;

namespace FreelanceManagementSystem.Services;

public class InvoiceService
{
    private List<Invoice> invoices;
    private int invoiceCounter;

    public InvoiceService()
    {
        invoices = new List<Invoice>();
        invoiceCounter = 1;
    }

    public InvoiceService(List<Invoice> existingInvoices)
    {
        invoices = existingInvoices ?? new List<Invoice>();
        invoiceCounter = invoices.Count + 1;
    }

    // FR5: Create Invoice (NEW in v2.0.0)
    public Invoice CreateInvoice(string projectId, string clientId, decimal amount, DateTime dueDate)
    {
        if (string.IsNullOrWhiteSpace(projectId))
        {
            throw new ArgumentException("Project ID cannot be empty.");
        }

        if (string.IsNullOrWhiteSpace(clientId))
        {
            throw new ArgumentException("Client ID cannot be empty.");
        }

        if (amount <= 0)
        {
            throw new ArgumentException("Invoice amount must be greater than zero.");
        }

        if (dueDate < DateTime.Now.Date)
        {
            throw new ArgumentException("Due date cannot be in the past.");
        }

        string invoiceNumber = $"INV-{DateTime.Now:yyyyMM}-{invoiceCounter:D4}";
        var invoice = new Invoice(invoiceNumber, projectId, clientId, amount, dueDate);
        invoices.Add(invoice);
        invoiceCounter++;
        return invoice;
    }

    // FR6: View Invoice Status (NEW in v2.0.0)
    public List<Invoice> GetAllInvoices()
    {
        return invoices;
    }

    public List<Invoice> GetUnpaidInvoices()
    {
        return invoices.Where(i => !i.IsPaid).ToList();
    }

    public List<Invoice> GetPaidInvoices()
    {
        return invoices.Where(i => i.IsPaid).ToList();
    }

    public List<Invoice> GetOverdueInvoices()
    {
        return invoices.Where(i => i.IsOverdue()).ToList();
    }

    public Invoice? GetInvoiceById(string id)
    {
        return invoices.FirstOrDefault(i => i.Id == id);
    }

    public Invoice? GetInvoiceByIndex(int index)
    {
        if (index >= 0 && index < invoices.Count)
        {
            return invoices[index];
        }
        return null;
    }

    public void MarkAsPaid(string invoiceId)
    {
        var invoice = invoices.FirstOrDefault(i => i.Id == invoiceId);
        if (invoice == null)
        {
            throw new ArgumentException("Invoice not found.");
        }

        invoice.IsPaid = true;
    }

    public decimal GetTotalRevenue()
    {
        return invoices.Where(i => i.IsPaid).Sum(i => i.Amount);
    }

    public decimal GetOutstandingAmount()
    {
        return invoices.Where(i => !i.IsPaid).Sum(i => i.Amount);
    }

    public int GetInvoiceCount()
    {
        return invoices.Count;
    }

    public void SetInvoices(List<Invoice> newInvoices)
    {
        invoices = newInvoices ?? new List<Invoice>();
        invoiceCounter = invoices.Count + 1;
    }
}
