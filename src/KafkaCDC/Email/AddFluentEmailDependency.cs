using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KafkaCDC.Email
{
    public static class AddFluentEmailDependency
    {
        public static IServiceCollection AddFluentEmailServices(this IServiceCollection services, IConfiguration configuration)
        {
            var emailSettings = new EmailSettings();
            configuration.GetSection(nameof(EmailSettings)).Bind(emailSettings);

            services.AddSingleton(emailSettings);
            services.AddFluentEmail(emailSettings.AdminEmail)
                .AddSmtpSender(new SmtpClient(emailSettings.MailServer, emailSettings.MailPort)
                {
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(emailSettings.AdminEmail, emailSettings.AdminPassword),
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                });
            services.AddScoped<IEmailService, EmailService>();
            return services;
        }
    }
}
