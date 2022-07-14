using Sesc.CrossCutting.Notification.Config;
using Sesc.CrossCutting.Notification.RabbitMQ;
using Sesc.CrossCutting.Notification.Services.Contracts;
using System;
using System.Threading.Tasks;

namespace Sesc.CrossCutting.Notification.Services
{
    public class MessageBus : IMessageBus
    {
        protected readonly MessageBusOptions _options;
        protected readonly IRabbitMqConnection _sender;

        public MessageBus(
            IRabbitMqConnection rabbitMqConnection
        )
        {
            _sender = rabbitMqConnection;
        }

        public void SendToQueue(String queue, String message)
        {
            _sender.SendToQueue(queue, message);
        }

        public void SendToQueue<T>(string queue, T message) where T : SerializableMessage
        {
            _sender.SendToQueue(queue, message.Serialize());
        }

        public async Task<string> RemoteCallAsync(string queue, string message)
        {
            return await Task.Run(() =>
            {
                return _sender.RemoteCall(queue, message);
            });
        }

        public void Dispose()
        {
            if (_sender != null)
            {
                _sender.Dispose();
            }
        }
    }
}
