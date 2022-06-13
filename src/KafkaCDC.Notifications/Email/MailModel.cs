using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Http;

namespace KafkaCDC.Notifications.Email
{
    public class MailModel
    {
        [Required]
        public string ToEmail { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Body { get; set; }
        public List<IFormFile> Attachments { get; set; }
    }
}
