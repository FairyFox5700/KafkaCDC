using KafkaCDC.Common.Kafka;
using KafkaCDC.Email;

namespace KafkaCDC.Notificator.Events.Handlers
{
    public class TraderCreatedEventHandler : IKafkaHandler<string, TraderCreatedEvent>
    {
        private readonly IEmailService _emailService;

        public TraderCreatedEventHandler(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task HandleAsync(string key, TraderCreatedEvent value)
        {
            if (string.IsNullOrEmpty(value.Email))
            {
                throw new InvalidDataException("Email mist not bu null or empty");
            }

            await _emailService.SendMessage(new MailModel()
            {
                Subject = "Hi",
                ToEmail = value.Email,
                Body = $"Welcome in our system {value.FirstName} +{value.LastName}",
            });
        }
    }
}
