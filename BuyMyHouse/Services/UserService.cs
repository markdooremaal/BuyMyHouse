using BuyMyHouse.Services.Abstract;
using DAL;
using Microsoft.EntityFrameworkCore;
using Models;

namespace BuyMyHouse.Services;

public class UserService : IUserService
{
    private readonly BuyMyHouseContext _buyMyHouseContext;

    public UserService(BuyMyHouseContext buyMyHouseContext)
    {
        _buyMyHouseContext = buyMyHouseContext;
    }
    
    public async Task<User> AddAsync(User request)
    {
        _buyMyHouseContext.Users.Add(request);
        await _buyMyHouseContext.SaveChangesAsync();

        return request;
    }

    public async Task<IEnumerable<User>> GetAllUsers()
    {
        return await _buyMyHouseContext.Set<User>().Include(x => x.Mortgage).ToListAsync();
    }

    public async Task<User> GetById(int id)
    {
        return await _buyMyHouseContext.Set<User>().Include(x => x.Mortgage).FirstOrDefaultAsync(x => x.UserId == id);
    }

    public async Task<User> GetByEmail(string email)
    {
        return await _buyMyHouseContext.Set<User>().Include(x => x.Mortgage).FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task RemoveAsync(User user)
    {
        _buyMyHouseContext.Users.Remove(user);
        await _buyMyHouseContext.SaveChangesAsync();
    }

    public async Task<User> UpdateAsync(User user, User updatedUser)
    {
        user.FirstName = updatedUser.FirstName;
        user.LastName = updatedUser.LastName;
        user.Email = updatedUser.Email;
        user.Income = updatedUser.Income;
        user.Mortgage = updatedUser.Mortgage;

        _buyMyHouseContext.Users.Update(user);
        await _buyMyHouseContext.SaveChangesAsync();

        return user;
    }
}