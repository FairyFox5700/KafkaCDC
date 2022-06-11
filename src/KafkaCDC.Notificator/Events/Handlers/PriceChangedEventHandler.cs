using KafkaCDC.Common.Kafka;
using KafkaCDC.Email;

namespace KafkaCDC.Events.Handlers
{
    internal class DealSubscribedTradersUpdatedEventHandler : IKafkaHandler<string, DealSubscribedTradersUpdatedEvent>
    {
        private readonly IEmailService _emailService;

        public DealSubscribedTradersUpdatedEventHandler(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task HandleAsync(string key, DealSubscribedTradersUpdatedEvent value)
        {
            if (value.EmailsList != null && value.EmailsList.Any())
            {
                var taskList = new List<Task>();
                foreach (var email in value.EmailsList)
                {
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
