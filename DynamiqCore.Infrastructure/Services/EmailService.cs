using Azure;
using Azure.Communication.Email;
using DynamiqCore.Domain.Interfaces;

namespace DynamiqCore.Infrastructure.Services;

public class EmailService : IEmailService
{
    #region Fields

    private readonly EmailClient _emailClient;
    private readonly string _senderEmail;

    #endregion

    #region Constructor

    public EmailService(EmailClient emailClient, string senderEmail)
    {
        _emailClient = emailClient ?? throw new ArgumentNullException(nameof(emailClient));
        _senderEmail = senderEmail ?? throw new ArgumentNullException(nameof(senderEmail));
    }

    #endregion

    #region Methods

    public async Task SendEmailAsync(string toEmail, string subject, string message)
    {

        try
        {
            var emailContent = new EmailContent(subject)
            {
                PlainText = message,
                Html = $"<strong>{message}</strong>"
            };

            var emailRecipients = new EmailRecipients(new List<EmailAddress>
            {
                new EmailAddress(toEmail)
            });

            var emailMessage = new EmailMessage(_senderEmail, emailRecipients, emailContent);

            // Send the email
            await _emailClient.SendAsync(WaitUntil.Completed, emailMessage);
        }
        catch (Exception exception)
        {
            Console.WriteLine($"Error sending email: {exception.Message}");
        }
    }

    #endregion

}