using KafkaCDC.Common.Kafka;
using KafkaCDC.Notifications.Email;

using Newtonsoft.Json;

namespace KafkaCDC.Notifications.Events.Handlers
{
    public class TraderCreatedEventHandler : IKafkaHandler<string, TraderCreatedEvent>
    {
        private readonly IEmailService _emailService;
        private readonly ILogger<TraderCreatedEventHandler> _logger;

        public TraderCreatedEventHandler(IEmailService emailService,
            ILogger<TraderCreatedEventHandler> logger)
        {
            _emailService = emailService;
            _logger = logger;
        }

        public async Task HandleAsync(string key, TraderCreatedEvent value)
        {
            _logger.LogWarning(JsonConvert.SerializeObject(value));
            if (string.IsNullOrEmpty(value.Email))
            {
                throw new InvalidDataException("Email must not bu null or empty");
            }

            await _emailService.SendMessage(new MailModel()
            {
                FromEmail = "admin@gmail.com",
                Subject = "Hi",
                ToEmail = value.Email,
                Body = $"Welcome in our system {value.FirstName} {value.LastName}",
            });
        }
    }
}
