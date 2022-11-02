using System.Threading.Tasks;
using AzureFunctions.Clients.Abstract;
using BuyMyHouse.Services.Abstract;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace AzureFunctions;

public class SendEmail
{
    private readonly IMailClient _mailClient;
    private readonly IUserService _userService;

    public SendEmail(IMailClient mailClient, IUserService userService)
    {
        _mailClient = mailClient;
        _userService = userService;
    }

    [FunctionName("SendEmail")]
    public async Task RunAsync([QueueTrigger("%QueueName%", Connection = "AzureWebJobsStorage")] string myQueueItem, ILogger log)
    {
        var user = _userService.GetById(int.Parse(myQueueItem)).Result;
        await _mailClient.SendMortgageOffer(user);
        
        log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
    }
}