using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

internal class RunnigService : IHostedService, IDisposable
{
    private Timer _timer;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Service starting.");

        _timer = new Timer(Start, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));

        return Task.CompletedTask;
    }

    private void Start(object state)
    {
        var activeKeyList = KeyModel.Model.keyLists.Where(s => s.State == true).ToList();
        foreach (var item in activeKeyList)
        {
            item.Description = string.Concat(DateTime.Now, " log sent.");
            QueueServiceFactory.Service.Sent("keyLog", item);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Service stopped.");

        _timer?.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}