using Betisa.Framework.Core.EventBusRabbitMQ_NS;
using Betisa.Framework.Core.EventBus.Abstractions;
using Betisa.Framework.Core.EventBus.Events;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace Betisa.Framework.Core.EventBus.EventBusRabbitMQ_NS
{
    public static class RabbitExtensions
    {

        public static IServiceCollection AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
           {
               var loggerMq = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();
               var factory = new ConnectionFactory()
               {
                   HostName = "localhost",
                   ///configuration["EventBus:HostName"],
                   DispatchConsumersAsync = true,
                   AutomaticRecoveryEnabled = true,
               };


               factory.Port = 5672;
               //factory.Port = configuration.GetValue<int>("EventBus:Port");
               factory.UserName = "guest";
               factory.Password = "guest";
               //if (!string.IsNullOrEmpty(configuration["EventBus:UserName"]))
               //{
               //    factory.UserName = configuration["EventBus:UserName"];
               //}

               //if (!string.IsNullOrEmpty(configuration["EventBus:Password"]))
               //{
               //    factory.Password = configuration["EventBus:Password"];
               //}

               //var retryCount = 5;
               //if (!string.IsNullOrEmpty(configuration["EventBus:RetryCount"]))
               //{
               //    retryCount = int.Parse(configuration["EventBus:RetryCount"]);
               //}

               return new DefaultRabbitMQPersistentConnection(factory, loggerMq, 5);
           });
            services.RegisterEventBus(configuration);
            return services;
        }

        public static IServiceCollection AddTestRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
           {
               var factory = new ConnectionFactory()
               {
                   HostName = "localhost",

                   //configuration["EventBus:HostName"],
                   DispatchConsumersAsync = true
               };
               factory.Port = 5672;
               factory.UserName = "guest";
               factory.Password = "guest";

               //factory.Port = configuration.GetValue<int>("EventBus:Port");

               //if (!string.IsNullOrEmpty(configuration["EventBus:UserName"]))
               //{
               //    factory.UserName = configuration["EventBus:UserName"];
               //}

               //if (!string.IsNullOrEmpty(configuration["EventBus:Password"]))
               //{
               //    factory.Password = configuration["EventBus:Password"];
               //}

               //var retryCount = 5;
               //if (!string.IsNullOrEmpty(configuration["EventBus:RetryCount"]))
               //{
               //    retryCount = int.Parse(configuration["EventBus:RetryCount"]);
               //}

               return new TestRabbitMQPersistentConnection();
           });
            services.RegisterTestEventBus(configuration);
            return services;
        }
        private static void RegisterEventBus(this IServiceCollection services, IConfiguration Configuration)
        {
            var subscriptionClientName = Configuration["EventBus:ClientName"];

            services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp =>
           {
               var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
               var iLifetimeScope = sp.GetRequiredService<Autofac.ILifetimeScope>();
               var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ>>();
               var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

               var retryCount = 5;
               if (!string.IsNullOrEmpty(Configuration["EventBus:RetryCount"]))
               {
                   retryCount = int.Parse(Configuration["EventBus:RetryCount"]);
               }

               return new EventBusRabbitMQ(rabbitMQPersistentConnection, logger,
                   iLifetimeScope, eventBusSubcriptionsManager, subscriptionClientName, retryCount);
           });

            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

            // services.AddTransient<ProductPriceChangedIntegrationEventHandler> ();
            // services.AddTransient<OrderStartedIntegrationEventHandler> ();
        }
        private static void RegisterTestEventBus(this IServiceCollection services, IConfiguration Configuration)
        {
            var subscriptionClientName = Configuration["EventBus:ClientName"];

            services.AddSingleton<IEventBus, TestEventBusRabbitMQ>(sp =>
           {
               var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
               var iLifetimeScope = sp.GetRequiredService<Autofac.ILifetimeScope>();
               var logger = sp.GetRequiredService<ILogger<TestEventBusRabbitMQ>>();
               var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

               var retryCount = 5;
               if (!string.IsNullOrEmpty(Configuration["EventBus:RetryCount"]))
               {
                   retryCount = int.Parse(Configuration["EventBus:RetryCount"]);
               }

               return new TestEventBusRabbitMQ(rabbitMQPersistentConnection, logger,
                   iLifetimeScope, eventBusSubcriptionsManager, subscriptionClientName, retryCount);
           });

            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

            // services.AddTransient<ProductPriceChangedIntegrationEventHandler> ();
            // services.AddTransient<OrderStartedIntegrationEventHandler> ();
        }

        public static EventBusSubscriber UseEventBus(this IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();

            return new EventBusSubscriber(eventBus);
        }
    }
    public class EventBusSubscriber
    {
        private readonly IEventBus bus;

        public EventBusSubscriber(IEventBus bus)
        {
            this.bus = bus;
        }

        public EventBusSubscriber SubscribeCommand<TCommand>()
        {
            // bus.Subscribe<TCommand,Command>()
            return this;
        }
        public EventBusSubscriber SubscribeEvent<TEvent, THEvent>()
        where TEvent : IntegrationEvent
        where THEvent : IIntegrationEventHandler<TEvent>
        {
            bus.Subscribe<TEvent, THEvent>();
            return this;
        }
    }
}