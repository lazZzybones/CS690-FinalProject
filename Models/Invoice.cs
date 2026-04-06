namespace FreelanceManagementSystem.Models;

public class Invoice
{
    public string Id { get; set; }
    public string InvoiceNumber { get; set; }
    public string ProjectId { get; set; }
    public string ClientId { get; set; }
    public decimal Amount { get; set; }
    public DateTime IssueDate { get; set; }
    public DateTime DueDate { get; set; }
    public bool IsPaid { get; set; }

    public Invoice()
    {
        Id = Guid.NewGuid().ToString();
        InvoiceNumber = string.Empty;
        ProjectId = string.Empty;
        ClientId = string.Empty;
        Amount = 0;
        IssueDate = DateTime.Now;
        DueDate = DateTime.Now.AddDays(30);
        IsPaid = false;
    }

    public Invoice(string invoiceNumber, string projectId, string clientId, decimal amount, DateTime dueDate)
    {
        Id = Guid.NewGuid().ToString();
        InvoiceNumber = invoiceNumber;
        ProjectId = projectId;
        ClientId = clientId;
        Amount = amount;
        IssueDate = DateTime.Now;
        DueDate = dueDate;
        IsPaid = false;
    }

    public bool IsOverdue()
    {
        return !IsPaid && DateTime.Now > DueDate;
    }

    public int GetDaysOverdue()
    {
        if (!IsOverdue()) return 0;
        return (DateTime.Now - DueDate).Days;
    }

    public string GetStatus()
    {
        if (IsPaid) return "PAID";
        if (IsOverdue()) return $"OVERDUE ({GetDaysOverdue()} days)";
        return "UNPAID";
    }

    public override string ToString()
    {
        return $"[{InvoiceNumber}] ${Amount:F2} | Due: {DueDate:yyyy-MM-dd} | {GetStatus()}";
    }

    public string GetDetailedInfo()
    {
        return $@"
╔══════════════════════════════════════════════════════════════════
║ INVOICE DETAILS
╠══════════════════════════════════════════════════════════════════
║ Invoice #:  {InvoiceNumber}
║ Amount:     ${Amount:F2}
║ Issue Date: {IssueDate:yyyy-MM-dd}
║ Due Date:   {DueDate:yyyy-MM-dd}
║ Status:     {GetStatus()}
║ Project ID: {ProjectId[..8]}...
║ Client ID:  {ClientId[..8]}...
╚══════════════════════════════════════════════════════════════════";
    }
}
