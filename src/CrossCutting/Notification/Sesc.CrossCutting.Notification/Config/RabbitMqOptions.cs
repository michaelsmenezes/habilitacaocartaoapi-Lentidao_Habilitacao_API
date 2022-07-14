using System;
using System.Collections.Generic;
using System.Text;

namespace Sesc.CrossCutting.Notification.Config
{
    public class RabbitMqOptions
    {
        public string Protocol { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string User { get; set; }
        public string Passwd { get; set; }
    }
}
