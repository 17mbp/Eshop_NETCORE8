using Carter;

var builder = WebApplication.CreateBuilder(args);

// ADD SERVICES CONTAINER
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(typeof(Program).Assembly);
});
var app = builder.Build();

// HTTP REQUEST
app.MapCarter();

app.Run();
