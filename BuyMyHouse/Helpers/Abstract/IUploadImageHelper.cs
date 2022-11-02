namespace BuyMyHouse.Helpers.Abstract;

public interface IUploadImageHelper
{
    public Task<string> Upload(IFormFile image);
}