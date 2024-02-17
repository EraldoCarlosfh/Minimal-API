public static class AppExtensions
{
    public static WebApplication UseArchitectures(this WebApplication app)
    {
        app.MapControllers();
        app.UseSwagger();
        app.UseSwaggerUI();

        return app;
    }
}
