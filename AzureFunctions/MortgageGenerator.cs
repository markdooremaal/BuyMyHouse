using System;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Queues;
using BuyMyHouse.Services.Abstract;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Models;


namespace AzureFunctions;

public class MortgageGenerator
{
    private readonly IUserService _userService;

    public MortgageGenerator(IUserService userService)
    {
        _userService = userService;
    }

    // should be 0 0 * * * -- For testing set at every 15 seconds
    [FunctionName("MortgageGenerator")]
    public async Task RunAsync([TimerTrigger("*/15 * * * * *")] TimerInfo myTimer, ILogger log)
    {
        log.LogInformation("[Started] generate mortgage function at: {UtcNow}", DateTime.UtcNow);

        var users = _userService.GetAllUsers().Result;

        foreach (var user in users)
        {
            if (user.Mortgage == null)
            {
                // Not a real mortgage calculation, monthlyPayment is based on: https://en.wikipedia.org/wiki/Fixed-rate_mortgage#Pricing
                double maxLoan = user.Income * 0.2;
                double interest = 4.0;
                double interestRate = interest / 100 / 12;
                double monthlyPayment = interestRate / (1 - Math.Pow(1 + interestRate, -360.00)) * 200000;

                // Add mortgage to user and save
                var newUser = user;
                newUser.Mortgage = new Mortgage()
                {
                    MaxLoan = Math.Round(maxLoan, 2),
                    MonthlyPayment = Math.Round(monthlyPayment, 2),
                    InterestRate = interest
                };

                await _userService.UpdateAsync(user, newUser);

                // Queue userId
                string connection = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
                string queue = Environment.GetEnvironmentVariable("QueueName");
                QueueClient qc = new QueueClient(connection, queue);
                await qc.SendMessageAsync(Convert.ToBase64String(Encoding.UTF8.GetBytes(newUser.UserId.ToString())));
            }
        }

        log.LogInformation("[Finished] generate mortgage function: {UtcNow}", DateTime.UtcNow);
    }
}