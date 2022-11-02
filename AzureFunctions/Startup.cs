using System;
using AzureFunctions.Clients;
using AzureFunctions.Clients.Abstract;
using BuyMyHouse.Services;
using BuyMyHouse.Services.Abstract;
using DAL;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.DependencyInjection;
using SendGrid.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(AzureFunctions.Startup))]
namespace AzureFunctions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 30));

            builder.Services.AddDbContext<BuyMyHouseContext>(options =>
                options.UseMySql(Environment.GetEnvironmentVariable("MySQL"), serverVersion));
            
            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddTransient<IMortgageService, MortgageService>();
            builder.Services.AddTransient<IMailClient, MailClient>();
            
            builder.Services.AddSendGrid(options =>
            {
                options.ApiKey = Environment.GetEnvironmentVariable("sendgridKey");
            });
            
            builder.Services.AddAzureClients(clientBuilder =>
            {
                clientBuilder.AddQueueServiceClient(Environment.GetEnvironmentVariable("AzureWebJobsStorage"));
            });
        }
    }
}

