using BuyMyHouse.Services.Abstract;
using DAL;
using Microsoft.EntityFrameworkCore;
using Models;

namespace BuyMyHouse.Services;

public class HouseService : IHouseService
{
    private readonly BuyMyHouseContext _buyMyHouseContext;

    public HouseService(BuyMyHouseContext buyMyHouseContext)
    {
        _buyMyHouseContext = buyMyHouseContext;
    }
    
    public async Task<House> AddAsync(House request)
    {
        _buyMyHouseContext.Houses.Add(request);
        await _buyMyHouseContext.SaveChangesAsync();

        return request;
    }

    public async Task<IEnumerable<House>> GetAllHouses()
    {
        return await _buyMyHouseContext.Set<House>().ToListAsync();
    }

    public async Task<House> GetById(int id)
    {
        var house = await _buyMyHouseContext.Set<House>().FirstOrDefaultAsync(x => x.HouseId == id);

        return house;
    }

    public async Task<House> GetByTitle(string title)
    {
        return await _buyMyHouseContext.Set<House>().FirstOrDefaultAsync(x => x.Title == title);
    }

    public async Task<IEnumerable<House>> GetWithinPriceRange(double min, double max)
    {
        return await _buyMyHouseContext.Set<House>().Where(x => x.Price >= min && x.Price <= max).ToListAsync();
    }

    public async Task RemoveAsync(House house)
    {
        _buyMyHouseContext.Houses.Remove(house);
        await _buyMyHouseContext.SaveChangesAsync();
    }

    public async Task<House> UpdateAsync(House house, House updatedHouse)
    {
        house.Title = updatedHouse.Title;
        house.Price = updatedHouse.Price;
        house.Street = updatedHouse.Street;
        house.Number = updatedHouse.Number;
        house.PostalCode = updatedHouse.PostalCode;
        house.City = updatedHouse.City;
        
        _buyMyHouseContext.Houses.Update(house);
        await _buyMyHouseContext.SaveChangesAsync();

        return house;
    }
}