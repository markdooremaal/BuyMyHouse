using BuyMyHouse.Services.Abstract;
using DAL;
using Microsoft.EntityFrameworkCore;
using Models;

namespace BuyMyHouse.Services;

public class MortgageService : IMortgageService
{
    private readonly BuyMyHouseContext _buyMyHouseContext;

    public MortgageService(BuyMyHouseContext buyMyHouseContext)
    {
        _buyMyHouseContext = buyMyHouseContext;
    }
    
    public async Task<Mortgage> GetById(int id)
    {
        return await _buyMyHouseContext.Set<Mortgage>().FirstOrDefaultAsync(x => x.MortgageId == id);
    }
}