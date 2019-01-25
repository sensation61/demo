using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;

namespace console_client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // keyler sistem üzerinde aktif ediliyor. 
            StateService stateService = new StateService().Active();

            // Aktif key'ler için log Service çalıştırılıyor. 
            await new HostBuilder()
               .ConfigureServices((hostContext, services) =>
               {
                   services.AddHostedService<RunnigService>();
               }).RunConsoleAsync();
        }
    }
}
