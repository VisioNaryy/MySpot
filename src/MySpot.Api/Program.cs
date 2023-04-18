using MySpot.Api.Extensions;
using MySpot.Logging.Extensions;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;
var environment = builder.Environment;

builder.UseSerilog();

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddApplication(configuration);
services.AddInfrastructure(configuration, environment);
services.AddDomain(configuration);

var app = builder.Build();

app.UseInfrastructure();

app.Run();

public partial class Program
{
    
}