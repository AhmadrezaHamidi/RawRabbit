using Autofac.Extensions.DependencyInjection;
using Betisa.Framework.Core.EventBus.EventBusRabbitMQ_NS;
using Microsoft.AspNetCore.Hosting;
using Sub.SampleApp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddRabbitMq(builder.Configuration)
    .AddTransient<BooksCreatedEventHandler>();

builder.Services.AddSwaggerGen();


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

app.UseEventBus()
           .SubscribeEvent<BooksCreatedEvent, BooksCreatedEventHandler>();
           //.SubscribeEvent<PublisherUpdatedEvent, PublisherUpdatedEventHandler>();


app.Run();