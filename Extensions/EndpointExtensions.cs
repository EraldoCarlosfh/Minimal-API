public static class EndpointExtensions
{ 
    public static WebApplication MapEndpoints(this WebApplication app)
    {
        app.MapCartItemEndpoints().MapProductEndpoints().MapUserEndpoints();

        return app;
    }    
}