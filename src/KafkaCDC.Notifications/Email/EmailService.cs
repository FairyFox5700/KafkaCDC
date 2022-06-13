using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentEmail.Core;

using Microsoft.Extensions.Logging;

namespace KafkaCDC.Notifications.Email
{
    public class EmailService : IEmailService
    {
        private readonly IFluentEmail _email;
        private readonly ILogger<EmailService> _logger;


        public EmailService(IFluentEmail email,
            ILogger<EmailService> logger)
        {
            _email = email;
            _logger = logger;
        }

        public async Task<bool> SendMessage(MailModel model)
        {
            try
            {
                var result = await _email
                    .To(model.ToEmail)
                    .Subject(model.Subject)
                    .Body(model.Body)
                    .SendAsync();
                if (!result.Successful)
                {
                    _logger.LogError("Failed to send an email.\n{Errors}",
                        string.Join(Environment.NewLine, result.ErrorMessages));
                }

                return result.Successful;
            }
            catch (Exception ex)
            {

                _logger.LogError($"{DateTime.Now}: Failed to send email notification ❌! ({ex.Message})");
                return false;
            }

        }
    }
}
