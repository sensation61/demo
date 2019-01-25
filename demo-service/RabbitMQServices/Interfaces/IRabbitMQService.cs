using System.Collections;
using System.Collections.Generic;

namespace demo_service.RabbitMQServices.Interfaces
{
    public interface IRabbitMQService
    {
        bool SendQueue(string key, object message);
        List<T> Recive<T>(string key, bool read);
        int ReciveCount(string key);
    }
}