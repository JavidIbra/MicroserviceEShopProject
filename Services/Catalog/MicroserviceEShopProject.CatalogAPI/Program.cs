using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarter();

builder.Services.AddMediatR(conf =>
{
    conf.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddMarten(opt =>
{
    opt.Connection(builder.Configuration.GetConnectionString("Database")!);
    opt.AutoCreateSchemaObjects = JasperFx.AutoCreate.All;
}).UseLightweightSessions();

var app = builder.Build();

app.MapCarter();

app.Run();