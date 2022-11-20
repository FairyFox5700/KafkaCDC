
using System.Net;
using System.Net.Mail;


namespace KafkaCDC.Notifications.Email
{
    public static class AddEmailDependency
    {
        public static IServiceCollection AddFluentEmailServices(this IServiceCollection services, IConfiguration configuration)
        {
            var emailSettings = new EmailSettings();
            configuration.GetSection(nameof(EmailSettings)).Bind(emailSettings);

            services.AddSingleton(emailSettings);
            services.AddScoped<IEmailService, EmailService>();
            return services;
        }
    }
}
