using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Betisa.Framework.Core.EventBus.EventBusRabbitMQ_NS;
using Microsoft.Extensions.Configuration;
using Publisher.Sample;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//var useRabbitMq = builder.Configuration.GetValue<bool>("UseRabbitMQ", true);
//if (builder.Environment.IsProduction())
//    useRabbitMq = true;
//else if (builder.Environment.EnvironmentName?.Equals("Integeration") == true)
//    useRabbitMq = false;

//var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
//var useTestRabbit = !string.IsNullOrEmpty(env) &&
//    (!env.In("Test", "Development") || Configuration.GetValue<bool>("UseRabbitMQ"));
//((useTestRabbit)
//
//
builder.Services.AddRabbitMq(builder.Configuration)
    .AddTransient<IPublisherEventService, PublisherEventService>();
//: services.AddTestRabbitMq(Configuration)

builder.Host
          .UseServiceProviderFactory(new AutofacServiceProviderFactory())
;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
