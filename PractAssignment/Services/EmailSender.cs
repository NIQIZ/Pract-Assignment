using System.Configuration;
using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using PractAssignment.Models;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace PractAssignment.Services;

public class EmailSender : IEmailSender
{
    private readonly ILogger _logger;
    private readonly IConfiguration _config;
    public EmailSender(
        ILogger<EmailSender> logger,
        IConfiguration config,
        IOptions<EmailCredentials> optionsAccessor
        )
    {
        _logger = logger;
        _config = config;
        Options = optionsAccessor.Value;
    }
    public EmailCredentials Options { get; }
    public async Task SendEmailAsync(string toEmail, string subject, string message)
    {
        await Execute(subject, message, toEmail);
    }

    public async Task<bool> Execute(string subject, string message, string toEmail)
    {
        try
        {
            MailMessage mailMessage = new MailMessage();

            SmtpClient smtpClient = new SmtpClient();
        
            mailMessage.From = new MailAddress(Options.Email);
            mailMessage.To.Add(toEmail);
            mailMessage.Subject = subject;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = message;

            smtpClient.Port = 587;
            smtpClient.Host = "smtp.office365.com";

            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(Options.Email, Options.Password);
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