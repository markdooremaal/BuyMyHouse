using System;
using System.Threading.Tasks;
using AzureFunctions.Clients.Abstract;
using Models;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace AzureFunctions.Clients;

public class MailClient : IMailClient
{
    private readonly ISendGridClient _sendGridClient;

    public MailClient(ISendGridClient sendGridClient)
    {
        _sendGridClient = sendGridClient;
    }

    public async Task SendMortgageOffer(User user)
    {
        var baseUrl = Environment.GetEnvironmentVariable("baseUrl");
        var url = baseUrl + $"Mortgage/GetMortgageById?id={user.Mortgage.MortgageId}";
        
        var msg = new SendGridMessage()
        {
            From = new EmailAddress("info@lepidoptera.nl", "Team BuyMyHouse"),
            Subject = "Your Mortgage offer from BuyMyHouse.com",
            PlainTextContent = $"View your mortgage offer here: {url}"
        };

        msg.AddTo(new EmailAddress(user.Email, user.FirstName));

        await _sendGridClient.SendEmailAsync(msg).ConfigureAwait(true);
    }
}