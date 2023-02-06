using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace PractAssignment.Services;

public class EmailSender : IEmailSender
{
    private readonly ILogger _logger;

    public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor,
        ILogger<EmailSender> logger)
    {
        Options = optionsAccessor.Value;
        _logger = logger;
    }

    public AuthMessageSenderOptions Options { get; } //Set with Secret Manager.

    public async Task SendEmailAsync(string toEmail, string subject, string message)
    {
        if (string.IsNullOrEmpty(Options.SendGridKey))
        {
            throw new Exception("Null SendGridKey");
        }

        await Execute(subject, message, toEmail);
    }

    public async Task<bool> Execute(string subject, string message, string toEmail)
    {
        try
        {
            MailMessage mailMessage = new MailMessage();

            SmtpClient smtpClient = new SmtpClient();

            mailMessage.From = new MailAddress("ffm-no-reply@outlook.com");
            mailMessage.To.Add(toEmail);
            mailMessage.Subject = subject;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = message;

            smtpClient.Port = 587;
            smtpClient.Host = "smtp.office365.com";

            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential("ffm-no-reply@outlook.com", "Freshfarmmarket2023");
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.Send(mailMessage);

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }

    }
}