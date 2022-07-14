using Microsoft.Extensions.Options;
using Sesc.CrossCutting.Notification.Config;
using Sesc.CrossCutting.Notification.ObjectNotification;
using Sesc.CrossCutting.Notification.Services.Contracts;
using System;
using System.Collections.Generic;

namespace Sesc.CrossCutting.Notification.Services
{
    public class EmailBus : IEmailBus
    {
        private readonly IMessageBus _messageBus;
        private readonly EmailQueueConfig _optionsQueue;

        public EmailBus(IMessageBus messageBus,
                        IOptions<EmailQueueConfig> optionsQueue,
                        IOptions<EmailServerConfig> emailServerConfig)
        {
            _messageBus = messageBus ?? throw new ArgumentNullException(nameof(messageBus));
            _optionsQueue = optionsQueue.Value ?? throw new ArgumentNullException(nameof(optionsQueue));
        }

        public void SendToQueue(string to, string subject, string message, IList<string> copiarPara)
        {
            var objectNotification = new EmailObjectNotification
            {
                EmailTo = to,
                Subject = subject,
                Message = message,
                Copias = copiarPara
            };

            _messageBus.SendToQueue<EmailObjectNotification>(_optionsQueue.QueueName, objectNotification);
        }
    }
}
