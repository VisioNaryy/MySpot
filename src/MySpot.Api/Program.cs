using MySpot.Api.Extensions;
using MySpot.Logging.Extensions;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

builder.UseSerilog();

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddApplication(configuration);
services.AddInfrastructure(configuration);
services.AddDomain(configuration);

var app = builder.Build();

app.UseInfrastructure();

app.Run();
