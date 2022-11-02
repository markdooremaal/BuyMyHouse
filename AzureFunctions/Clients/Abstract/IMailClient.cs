using System.Threading.Tasks;
using Models;

namespace AzureFunctions.Clients.Abstract;

public interface IMailClient
{
    public Task SendMortgageOffer(User user);
}