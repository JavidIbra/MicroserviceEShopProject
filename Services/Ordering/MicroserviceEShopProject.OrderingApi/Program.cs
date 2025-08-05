using MicroserviceEShopProject.Ordering.Application;
using MicroserviceEShopProject.Ordering.Infrastructure;
using MicroserviceEShopProject.OrderingApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices();

var app = builder.Build();

app.Run();
