using System;
using MicroTest1.EventBusRabbitMQ_NS;
using RabbitMQ.Client;

namespace MicroTest1.EventBus.EventBusRabbitMQ
{
    public class TestRabbitMQPersistentConnection : IRabbitMQPersistentConnection
    {
        private bool connectionState = false;
        public bool IsConnected => connectionState;

        public event EventHandler ReConnected;

        public IModel CreateModel()
        {
            return new object() as IModel;
        }

        public void Dispose()
        {
            connectionState = false;
        }

        public bool TryConnect()
        {
            connectionState = true;
            return true;
        }
    }
}