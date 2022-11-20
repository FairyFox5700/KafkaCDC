using MailKit.Net.Smtp;

namespace KafkaCDC.Notifications.Email
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;
        private readonly ILogger<EmailService> _logger;
        public EmailService(EmailSettings emailSettings,
            ILogger<EmailService> logger)
        {
            _logger = logger;
            _emailSettings = emailSettings;
        }

        public async Task<bool> SendMessage(MailModel model)
        {
            try
            {
                var message = EmailModelBuilder.CreateMailMessage(model);
                using var client = new SmtpClient();
                await client.ConnectAsync(_emailSettings.MailServer, _emailSettings.MailPort, MailKit.Security.SecureSocketOptions.Auto);
                if (AreCredentialsDefined())
                {
                    await client.AuthenticateAsync(
                        _emailSettings.AdminEmail,
                        _emailSettings.AdminPassword);
                }
                var result =  await client.SendAsync(message).ConfigureAwait(false);
                await client.DisconnectAsync(true).ConfigureAwait(false);
                _logger.LogInformation($"{nameof(EmailService)}.{nameof(SendMessage)}.finished", "Finished email sending");
                return true;
            }
            catch (Exception ex)
            {

                _logger.LogError($"{DateTime.Now}: Failed to send email notification ❌! ({ex.Message})");
                return false;
            }
        }

        private bool AreCredentialsDefined()
        {
            return !string.IsNullOrEmpty(_emailSettings.AdminEmail) &&
                   !string.IsNullOrEmpty(_emailSettings.AdminPassword);
        }
    }
}
