namespace FixTimeBack.Service;

using Interfaces;
using Custom;

using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;



public class EmailService : IEmailService
{
    private readonly EmailSettings _emailSettings;

    public EmailService(EmailSettings emailSettings)
    {
        _emailSettings = emailSettings;
    }

    public async Task SendEmailAsync(string to, string subject, string body, bool isHtml = false)
    {
        var email = new MimeMessage();
        email.From.Add(new MailboxAddress(_emailSettings.FromName, _emailSettings.FromEmail));
        email.To.Add(MailboxAddress.Parse(to));
        email.Subject = subject;

        var builder = new BodyBuilder();
        if (isHtml)
            builder.HtmlBody = body;
        else
            builder.TextBody = body;

        email.Body = builder.ToMessageBody();

        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.SmtpPort, SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(_emailSettings.SmtpUsername, _emailSettings.SmtpPassword);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }
}