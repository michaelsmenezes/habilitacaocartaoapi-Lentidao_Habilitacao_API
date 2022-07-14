using System;
using System.Collections.Generic;
using System.Text;

namespace Sesc.CrossCutting.Notification.Services.Contracts
{
    public interface INotificationService
    {
        bool EnviarEmail(string to, string assunto, string msg, Dictionary<string, string> chavesMsg, IList<string> copiarPara = null);
    }
}
