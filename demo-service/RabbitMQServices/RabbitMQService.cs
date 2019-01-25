using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using demo_service.RabbitMQServices.Configurations;
using demo_service.RabbitMQServices.Interfaces;
using Newtonsoft.Json;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace demo_service.RabbitMQServices
{
    public class RabbitMQService : IRabbitMQService
    {
        private ConnectionFactory factory = null;
        public RabbitMQService()
        {
            factory = new ConnectionFactory();

            factory.HostName = RabbitMQConfiguration.HostName;
            factory.UserName = RabbitMQConfiguration.UserName;
            factory.Password = RabbitMQConfiguration.Password;
            factory.VirtualHost = RabbitMQConfiguration.VirtualHost;
            factory.Port = RabbitMQConfiguration.Port;
        }

        public bool SendQueue(string key, object message)
        {
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: key, durable: false, exclusive: false,
                autoDelete: false, arguments: null);

                var messageJson = JsonConvert.SerializeObject(message);
                var body = Encoding.UTF8.GetBytes(messageJson);

                channel.BasicPublish(exchange: "", routingKey: key,
                                    basicProperties: null,
                                    body: body);
            }
            return true;
        }
        public int ReciveCount(string key)
        {
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                var queueDeclareOK = channel.QueueDeclarePassive(key);
                return (int)queueDeclareOK.MessageCount;
            }
        }

        public List<T> Recive<T>(string key, bool read=true)
        {
            var res = new List<T>();

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {

                var queueDeclareOK = channel.QueueDeclarePassive(key);
                var consumerCount = queueDeclareOK.MessageCount;

                string resultStr = null;
                BasicGetResult result = null;
                for (int i = 0; i < consumerCount; i++)
                {
                    result = channel.BasicGet(key, false);

                    if (result != null)
                    {
                        resultStr = Encoding.UTF8.GetString(result.Body);
                        res.Add(JsonConvert.DeserializeObject<T>(resultStr));
                    }
                }
                
                if (read) channel.BasicAck(result.DeliveryTag, false);
            }

            return res;
        }
    }
}