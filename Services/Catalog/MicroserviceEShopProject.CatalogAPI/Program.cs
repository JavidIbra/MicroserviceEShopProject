using HealthChecks.UI.Client;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarter();

var assembly = typeof(Program).Assembly;

builder.Services.AddMediatR(conf =>
{
    conf.RegisterServicesFromAssembly(assembly);
    conf.AddOpenBehavior(typeof(ValidationBehavior<,>));
    conf.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddMarten(opt =>
{
    opt.Connection(builder.Configuration.GetConnectionString("Database")!);
    opt.AutoCreateSchemaObjects = JasperFx.AutoCreate.All;
}).UseLightweightSessions();

if (builder.Environment.IsDevelopment())
    builder.Services.InitializeMartenWith<CatalogInitialData>();

#region ExceptionHandling
builder.Services.AddExceptionHandler<ProblemExceptionHandler>();
builder.Services.AddProblemDetails(opt =>
{
    opt.CustomizeProblemDetails = context =>
    {
        context.ProblemDetails.Instance =
            $"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path}";

        context.ProblemDetails.Extensions.TryAdd("requestId", context.HttpContext.TraceIdentifier);

        var activity = context.HttpContext.Features.Get<IHttpActivityFeature>()?.Activity;
        context.ProblemDetails.Extensions.TryAdd("traceId", activity?.Id);
    };
});
#endregion

builder.Services.AddHealthChecks()
             .AddNpgSql(builder.Configuration.GetConnectionString("Database")!);


var app = builder.Build();

app.MapCarter();

app.UseExceptionHandler();

app.UseHealthChecks("/health" , 
    new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });   

app.Run();