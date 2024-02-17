
var builder = WebApplication.CreateBuilder(args);

builder.AddArchitectures()
       .AddServices()
       .AddSwagger()
       .AddRepositories();

var app = builder.Build();

app.MapEndpoints();

app.UseArchitectures();

app.Run();
