using MicroserviceEShopProject.Ordering.Application;
using MicroserviceEShopProject.Ordering.Infrastructure;
using MicroserviceEShopProject.Ordering.Infrastructure.Data.Extensions;
using MicroserviceEShopProject.OrderingApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplicationServices(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices(builder.Configuration);

var app = builder.Build();

app.UseApiServices();

if (app.Environment.IsDevelopment())
{
    await app.InitializeDatabaseAsync();
}

app.Run();
