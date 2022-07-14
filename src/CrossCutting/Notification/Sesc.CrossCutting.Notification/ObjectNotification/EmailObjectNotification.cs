using Sesc.CrossCutting.Notification.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sesc.CrossCutting.Notification.ObjectNotification
{
    public class EmailObjectNotification : SerializableMessage
    {
        public string EmailFrom { get; set; }
        public string EmailTo { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public IList<string> Copias { get; set; }
    }
}
