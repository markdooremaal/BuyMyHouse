using Models;

namespace BuyMyHouse.Services.Abstract;

public interface IUserService
{
    public Task<User> AddAsync(User request);
    
    public Task<IEnumerable<User>> GetAllUsers();
    
    public Task<User> GetById(int id);

    public Task<User> GetByEmail(string email);

    public Task RemoveAsync(User user);
    
    public Task<User> UpdateAsync(User user, User updatedUser);
}