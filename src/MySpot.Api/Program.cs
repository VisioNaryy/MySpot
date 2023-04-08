using MySpot.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddApplication(configuration);
services.AddInfrastructure(configuration);
services.AddDomain(configuration);

var app = builder.Build();

app.UseInfrastructure();

app.Run();
