using System;

namespace demo_service.RabbitMQServices.Configurations
{
    public class RabbitMQConfiguration
    {
        public static string UserName => Environment.GetEnvironmentVariable("Q_UserName");
        public static string Password => Environment.GetEnvironmentVariable("Q_Password");
        public static string HostName => Environment.GetEnvironmentVariable("Q_HostName");
        public static string VirtualHost => Environment.GetEnvironmentVariable("Q_VirtualHost");
        public static int Port => int.Parse(Environment.GetEnvironmentVariable("Q_Port"));

    }
}