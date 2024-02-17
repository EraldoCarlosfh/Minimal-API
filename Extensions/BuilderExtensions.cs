using ApiPetChaoBicho.Data;
using Microsoft.OpenApi.Models;

public static class BuilderExtensions
{
    public static WebApplicationBuilder AddArchitectures(this WebApplicationBuilder builder) 
    {
        builder.Services.AddControllers();
        builder.Services.AddDbContext<AppDbContext>();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddCors();
        return builder;
    }

    public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
    {
        return builder;
    }

    public static WebApplicationBuilder AddRepositories(this WebApplicationBuilder builder)
    {  
        return builder;
    }

    public static WebApplicationBuilder AddSwagger(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwagger();

        return builder;
    }

    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo()
            {
                Description = "API PetShop",
                Title = "Minimal API PetShop",
                Version = "v1",
                Contact = new OpenApiContact()
                {
                    Name = "CLD System",
                    Url = new Uri("https://www.cldsystem.com.br"),
                    Email = "contato@cldsystem.com.br"
                }
            });
        });

        return services;
    }
}
