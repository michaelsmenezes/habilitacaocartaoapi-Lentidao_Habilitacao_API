using System;

namespace Sesc.CrossCutting.Notification.RabbitMQ
{
    public interface IRabbitMqConnection
    {
        void SendToQueue(string queue, string message);

        string RemoteCall(string queue, string message, int timeout =10);

        void Dispose();
    }
}
