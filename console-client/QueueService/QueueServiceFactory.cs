using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

public class QueueServiceFactory
{
    private static QueueServiceFactory _service = null;
    private ConnectionFactory factory = null;
    public static QueueServiceFactory Service
    {
        get
        {
            if (_service == null)
            {
                _service = new QueueServiceFactory();
            }
            return _service;
        }
    }

    QueueServiceFactory()
    {
        factory = new ConnectionFactory() { HostName = "localhost" };
        factory.UserName = "admin";
        factory.Password = "123456";
        Console.Write(factory);
    }


    public T Revice<T>(string key)
    {
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {

            var queueDeclareOK = channel.QueueDeclarePassive(key);
            var consumerCount = queueDeclareOK.MessageCount;

            string resultStr = null;
            BasicGetResult result = null;
            for (int i = 0; i < 1; i++)
            {
                result = channel.BasicGet(key, false);

                if (result == null)
                {
                    Console.Write("// No message");
                }
                else
                {
                    resultStr = Encoding.UTF8.GetString(result.Body);
                }
            }

            channel.BasicAck(result.DeliveryTag, false);
            return JsonConvert.DeserializeObject<T>(resultStr);
        }
    }
    public void Sent(string key, object message)
    {
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: key, durable: false, exclusive: false,
            autoDelete: false, arguments: null);

            var jsonMessage = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(jsonMessage);

            channel.BasicPublish(exchange: "", routingKey: key, basicProperties: null, body: body);
            Console.WriteLine(" Sent {0}", jsonMessage);
        }
    }
}