using TimeMatt.Models;

namespace TimeMatt.Services;

public class AccountService : IAccountService
{
    private readonly IClientService _clientService;

    public AccountService(IClientService clientService)
    {
        _clientService = clientService;
    }

    public List<Account> GetAll()
    {
        var today = DateTime.Today;
        var accounts = new List<Account>
        {
            new() { Id = 1, Name = "Alex Morgan", Email = "alex.morgan@TextMatt.dev", Avatar = "AM", Role = AccountRole.Owner, Company = "Morgan Digital Studio", LastActive = today },
            new() { Id = 2, Name = "Jordan Lee", Email = "jordan.lee@TextMatt.dev", Avatar = "JL", Role = AccountRole.TeamMember, Company = "Morgan Digital Studio", LastActive = today.AddHours(-3) },
            new() { Id = 3, Name = "Maya Chen", Email = "maya.chen@TextMatt.dev", Avatar = "MC", Role = AccountRole.TeamMember, Company = "Morgan Digital Studio", LastActive = today.AddDays(-1) },
        };

        var nextId = accounts.Count + 1;
        foreach (var client in _clientService.GetAll())
        {
            accounts.Add(new Account
            {
                Id = nextId++,
                Name = client.Name,
                Email = client.Email,
                Avatar = client.Avatar,
                Role = AccountRole.Client,
                Company = client.Company,
                LastActive = client.ClientSince.AddDays(30)
            });
        }

        return accounts;
    }
}
