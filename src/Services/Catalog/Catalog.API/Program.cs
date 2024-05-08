using Carter;
using Marten;

var builder = WebApplication.CreateBuilder(args);

// ADD SERVICES CONTAINER
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(typeof(Program).Assembly);
});

builder.Services.AddMarten(opt =>
{
    opt.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

var app = builder.Build();

// HTTP REQUEST
app.MapCarter();

app.Run();