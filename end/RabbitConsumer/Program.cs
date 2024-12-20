// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitConsumer.Processors;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();

builder.AddRabbitMQClient("messaging");

builder.Services.AddHostedService<CatalogProcessingJob>();

var host = builder.Build();

host.Run();
