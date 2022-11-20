using MimeKit;

namespace KafkaCDC.Notifications.Email
{
    public class EmailModelBuilder
    {
        public static MimeMessage CreateMailMessage(MailModel email)
        {
            var message = new MimeMessage();
            message.Subject = email.Subject;
            message.From.Add(MailboxAddress.Parse(email.FromEmail));
            message.To.Add(MailboxAddress.Parse(email.ToEmail));
            var builder = new BodyBuilder
            {
                HtmlBody = email.Body,
            };

            if (email.Attachments != null && email.Attachments.Any())
            {
                foreach (var attachment in email.Attachments)
                {
                    builder.Attachments.Add(attachment.FileName, attachment.Data, ContentType.Parse(attachment.ContentType));
                }
            }

            message.Body = builder.ToMessageBody();

            return message;
        }
    }
}
