using Azure.Storage.Blobs;
using BuyMyHouse.Helpers.Abstract;

namespace BuyMyHouse.Helpers;

public class UploadImageHelper : IUploadImageHelper
{
    private readonly BlobServiceClient _blobServiceClient;

    public UploadImageHelper(BlobServiceClient blobServiceClient)
    {
        _blobServiceClient = blobServiceClient;
    }
    
    private string GenerateName(string original)
    {
        Random rnd = new Random();
        string filename = rnd.Next() + Path.GetExtension(original);

        return filename;
    }

    public async Task<string> Upload(IFormFile image)
    {
        var container = _blobServiceClient.GetBlobContainerClient("sspcontainer");
        var blob = container.GetBlobClient(GenerateName(image.FileName));
        await blob.UploadAsync(image.OpenReadStream());

        return blob.Uri.ToString();
    }
}