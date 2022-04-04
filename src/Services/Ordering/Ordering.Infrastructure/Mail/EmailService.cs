using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Models;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Ordering.Infrastructure.Mail;

public class EmailService : IEmailService
{
	private EmailSettings _settings;
	private ILogger<IEmailService> _logger;

	public EmailService(IOptions<EmailSettings> settings, ILogger<IEmailService> logger)
	{
		_logger = logger;
		_settings = settings.Value;
	}

	public async Task<bool> Send(Email message)
	{
		var client = new SendGridClient(_settings.ApiKey);
		var from = new EmailAddress(_settings.FromAddress, _settings.FromName);
		var to = new EmailAddress(message.To);
		var gridMessage = MailHelper.CreateSingleEmail(from, to, message.Subject, message.Body, "");
		var response = await client.SendEmailAsync(gridMessage);

		_logger.LogInformation("Email send {@GridMessage}", gridMessage);
		return response.IsSuccessStatusCode;
	}
}