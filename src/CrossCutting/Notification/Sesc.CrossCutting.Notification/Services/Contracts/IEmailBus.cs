using System.Collections.Generic;

namespace Sesc.CrossCutting.Notification.Services.Contracts
{
    public interface IEmailBus
    {
        void SendToQueue(string to, string subject, string messageBody, IList<string> copiarPara);
    }
}
