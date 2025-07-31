using MicroserviceEShopProject.BuildingBlocks.Behaviors;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarter();

var assembly = typeof(Program).Assembly;

builder.Services.AddMediatR(conf =>
{
    conf.RegisterServicesFromAssembly(assembly);
    conf.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddMarten(opt =>
{
    opt.Connection(builder.Configuration.GetConnectionString("Database")!);
    opt.AutoCreateSchemaObjects = JasperFx.AutoCreate.All;
}).UseLightweightSessions();

var app = builder.Build();

app.MapCarter();

app.Run();