using TimeMatt.Models;

namespace TimeMatt.Services;

public interface IClientService
{
    List<Client> GetAll();
    Client? GetById(int id);
    Client Add(string name, string company, string email);
}
