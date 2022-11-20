using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Http;

using MimeKit;

namespace KafkaCDC.Notifications.Email
{
    public class MailModel
    {
        [Required]
        public string ToEmail { get; set; } = "";
        [Required]
        public string Subject { get; set; } = "";
        [Required]
        public string Body { get; set; } = "";
        public Attachment[]? Attachments { get; set; }
        public string FromEmail { get; set; } = "";
    }
    public class Attachment
    {
        public string FileName { get; set; } = "";
        public Stream? Data { get; set; }
        public string ContentType { get; set; } = "";
    }
}
