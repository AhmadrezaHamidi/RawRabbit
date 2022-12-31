using System;
using System.Threading.Tasks;
using Autofac;
using MicroTest1.EventBus.Abstractions;
using MicroTest1.EventBus.Events;
using MicroTest1.EventBusRabbitMQ_NS;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MicroTest1.EventBus.EventBusRabbitMQ
{
    public class TestEventBusRabbitMQ : IEventBus, IDisposable
    {
        private readonly ILogger<TestEventBusRabbitMQ> _logger;
        const string BROKER_NAME = "eshop_event_bus";

        private readonly IRabbitMQPersistentConnection _persistentConnection;
        private readonly IEventBusSubscriptionsManager _subsManager;
        private readonly ILifetimeScope _autofac;
        private readonly string AUTOFAC_SCOPE_NAME = "eshop_event_bus";
        private readonly int _retryCount;

        private IModel _consumerChannel;
        private string _queueName;
        public TestEventBusRabbitMQ(IRabbitMQPersistentConnection persistentConnection, ILogger<TestEventBusRabbitMQ> logger,
           ILifetimeScope autofac, IEventBusSubscriptionsManager subsManager, string queueName = null, int retryCount = 5)
        {
            _persistentConnection = persistentConnection ?? throw new ArgumentNullException(nameof(persistentConnection));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _subsManager = subsManager ?? new InMemoryEventBusSubscriptionsManager();
            _queueName = queueName;
            _consumerChannel = CreateConsumerChannel();
            _autofac = autofac;
            _retryCount = retryCount;
            _subsManager.OnEventRemoved += SubsManager_OnEventRemoved;
        }


        private void SubsManager_OnEventRemoved(object sender, string eventName)
        {
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }

        }



        private IModel CreateConsumerChannel()
        {
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }

            _logger.LogTrace("Creating RabbitMQ consumer channel");

            var channel = _persistentConnection.CreateModel();
            if (channel is null)
                return null;
            channel.ExchangeDeclare(exchange: BROKER_NAME,
                                    type: "direct");

            channel.QueueDeclare(queue: _queueName,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            channel.CallbackException += (sender, ea) =>
            {
                _logger.LogWarning(ea.Exception, "Recreating RabbitMQ consumer channel");

                _consumerChannel.Dispose();
                _consumerChannel = CreateConsumerChannel();
                StartBasicConsume();
            };

            return channel;
        }

        private void StartBasicConsume()
        {
            _logger.LogTrace("Starting RabbitMQ basic consume");

            if (_consumerChannel != null)
            {
                var consumer = new AsyncEventingBasicConsumer(_consumerChannel);

                consumer.Received += Consumer_Received;

                _consumerChannel.BasicConsume(
                    queue: _queueName,
                    autoAck: false,
                    consumer: consumer);
            }
            else
            {
                _logger.LogError("StartBasicConsume can't call on _consumerChannel == null");
            }
        }

        private async Task Consumer_Received(object sender, BasicDeliverEventArgs eventArgs)
        {
            await Task.CompletedTask;
        }

        public void Dispose()
        {
        }

        public void Publish(IntegrationEvent @event)
        {
            _logger.LogInformation("message from test rabbigtmq for {@event}", @event);
        }

        public void Subscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            _logger.LogInformation("message from test rabbigtmq for Subscribe ");

        }

        public void SubscribeDynamic<TH>(string eventName) where TH : IDynamicIntegrationEventHandler
        {
            _logger.LogInformation("message from test rabbigtmq for SubscribeDynamic ");

        }

        public void Unsubscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
        }

        public void UnsubscribeDynamic<TH>(string eventName) where TH : IDynamicIntegrationEventHandler
        {
            _logger.LogInformation("message from test rabbigtmq for UnsubscribeDynamic ");

        }
    }
}