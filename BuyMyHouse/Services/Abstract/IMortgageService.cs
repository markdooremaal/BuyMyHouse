using Models;

namespace BuyMyHouse.Services.Abstract;

public interface IMortgageService
{
    public Task<Mortgage> GetById(int id);
    public Task DeleteAll();
}