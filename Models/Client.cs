namespace FreelanceManagementSystem.Models;

public class Client
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Company { get; set; }
    public DateTime CreatedDate { get; set; }

    public Client()
    {
        Id = Guid.NewGuid().ToString();
        Name = string.Empty;
        Email = string.Empty;
        Phone = string.Empty;
        Company = string.Empty;
        CreatedDate = DateTime.Now;
    }

    public Client(string name, string email, string phone, string company)
    {
        Id = Guid.NewGuid().ToString();
        Name = name;
        Email = email;
        Phone = phone;
        Company = company;
        CreatedDate = DateTime.Now;
    }

    public override string ToString()
    {
        return $"[{Id[..8]}] {Name} | {Company} | {Email} | {Phone}";
    }

    public string GetDetailedInfo()
    {
        return $@"
╔══════════════════════════════════════════════════════════════════
║ CLIENT DETAILS
╠══════════════════════════════════════════════════════════════════
║ ID:       {Id}
║ Name:     {Name}
║ Company:  {Company}
║ Email:    {Email}
║ Phone:    {Phone}
║ Added:    {CreatedDate:yyyy-MM-dd}
╚══════════════════════════════════════════════════════════════════";
    }
}
