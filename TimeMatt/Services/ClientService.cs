using TimeMatt.Models;

namespace TimeMatt.Services;

public class ClientService
{
    private readonly List<Client> _clients;

    public ClientService()
    {
        var today = DateTime.Today;
        _clients = new List<Client>
        {
            new() { Id = 1, Name = "Sarah Bennett", Company = "Acme Studio", Email = "sarah@acmestudio.com", Phone = "+1 (415) 555-0132", Avatar = "SB", Address = "San Francisco, CA", ClientSince = today.AddMonths(-14) },
            new() { Id = 2, Name = "Marcus Lee", Company = "Pixel Labs", Email = "marcus@pixellabs.io", Phone = "+1 (206) 555-0198", Avatar = "ML", Address = "Seattle, WA", ClientSince = today.AddMonths(-9) },
            new() { Id = 3, Name = "Elena Voss", Company = "Creative Media", Email = "elena@creativemedia.co", Phone = "+49 30 5550 1122", Avatar = "EV", Address = "Berlin, Germany", ClientSince = today.AddMonths(-20) },
            new() { Id = 4, Name = "Daniel Cho", Company = "Nova Retail", Email = "daniel@novaretail.com", Phone = "+1 (312) 555-0177", Avatar = "DC", Address = "Chicago, IL", ClientSince = today.AddMonths(-5) },
            new() { Id = 5, Name = "Priya Nair", Company = "Bright Path Consulting", Email = "priya@brightpath.com", Phone = "+44 20 7946 0958", Avatar = "PN", Address = "London, UK", ClientSince = today.AddMonths(-3) },
        };
    }

    public List<Client> GetAll() => _clients;

    public Client? GetById(int id) => _clients.FirstOrDefault(c => c.Id == id);
}
