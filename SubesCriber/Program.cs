using MicroTest1;
using MicroTest1.Messages.Statics;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Logging;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using MicroTest1.EventBus.EventBusRabbitMQ;
using Microsoft.Extensions.Configuration;
using MicroTest1.Controllers;
using Autofac.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddSeq(builder.Configuration.GetSection("Seq"));
});


var useRabbitMq = builder.Configuration.GetValue<bool>("UseRabbitMQ", true);
if (builder.Environment.IsProduction())
    useRabbitMq = true;
else if (builder.Environment.EnvironmentName?.Equals("Integeration") == true)
    useRabbitMq = false;

((useRabbitMq)
    ? builder.Services.AddRabbitMq(builder.Configuration).AddTransient<IActivityEventService, ActivityEventService>()
    : builder.Services.AddTestRabbitMq(builder.Configuration)
).AddTransient<IActivityEventService, ActivityEventService>();




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
