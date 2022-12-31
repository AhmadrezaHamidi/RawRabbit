using RabbitMQ.Client;
using System;

namespace MicroTest1.EventBusRabbitMQ_NS
{
    public interface IRabbitMQPersistentConnection
        : IDisposable
    {
        bool IsConnected { get; }

        bool TryConnect();

        event EventHandler ReConnected;

        IModel CreateModel();
    }
}
