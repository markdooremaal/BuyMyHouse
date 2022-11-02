using Models;

namespace BuyMyHouse.Services.Abstract;

public interface IHouseService
{
    public Task<House> AddAsync(House request);
    
    public Task<IEnumerable<House>> GetAllHouses();
    
    public Task<House> GetById(int id);

    public Task<House> GetByTitle(string title);

    public Task<IEnumerable<House>> GetWithinPriceRange(double min, double max);

    public Task RemoveAsync(House house);
    
    public Task<House> UpdateAsync(House house, House updatedHouse);
}