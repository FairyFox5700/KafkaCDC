using KafkaCDC.Common.Kafka;
using KafkaCDC.Notifications.Email;
using KafkaCDC.Notifications.Events;

namespace KafkaCDC.Notifications.Events.Handlers
{
    internal class DealSubscribedTradersUpdatedEventHandler : IKafkaHandler<string, DealSubscribedTradersUpdatedEvent>
    {
        private readonly IEmailService _emailService;
        private readonly ILogger<DealSubscribedTradersUpdatedEventHandler> _logger;

        public DealSubscribedTradersUpdatedEventHandler(IEmailService emailService,
            ILogger<DealSubscribedTradersUpdatedEventHandler> logger)
        {
            _emailService = emailService;
            _logger = logger;
        }

        public async Task HandleAsync(string key, DealSubscribedTradersUpdatedEvent value)
        {
            if (value.EmailsList != null && value.EmailsList.Any())
            {
                var taskList = new List<Task>();
                foreach (var email in value.EmailsList)
                {
                    _logger.LogWarning(email);
                    taskList.Add(_emailService.SendMessage(new MailModel()
                    {
                        ToEmail = email,
                        Subject = "Deal price changed",
                        Body = $"Deal: {value.DealId}. Changed low price: {value.RevisedPriceRangeLow}." +
                               $"Changed high price: {value.RevisedPriceRangeHigh}"

                    }));
                }

                await Task.WhenAll(taskList);
            }
        }
    }
}
