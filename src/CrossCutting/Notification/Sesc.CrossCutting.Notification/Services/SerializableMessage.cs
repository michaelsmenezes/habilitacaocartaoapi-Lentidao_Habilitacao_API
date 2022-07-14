using Newtonsoft.Json;

namespace Sesc.CrossCutting.Notification.Services
{
    public class SerializableMessage
    {
        public virtual string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
