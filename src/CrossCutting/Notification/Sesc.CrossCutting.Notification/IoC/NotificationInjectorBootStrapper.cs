using Microsoft.Extensions.DependencyInjection;
using Sesc.CrossCutting.Notification.RabbitMQ;
using Sesc.CrossCutting.Notification.Services;
using Sesc.CrossCutting.Notification.Services.Contracts;

namespace Sesc.CrossCutting.Notification.IoC
{
    public class NotificationInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<IRabbitMqConnection, RabbitMqConnection>();
            services.AddTransient<IMessageBus, MessageBus>();
            services.AddTransient<IEmailBus, EmailBus>();
            services.AddTransient<INotificationService, NotificationService>();
            services.AddHostedService<EmailQueueListener>();            
            
        }
    }
}
