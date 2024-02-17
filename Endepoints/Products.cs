using ApiPetChaoBicho.Data;
using ApiPetChaoBicho.Data.ViewModels;
using Microsoft.EntityFrameworkCore;

public static class Products
{
    public static WebApplication MapProductEndpoints(this WebApplication app)
    {
        app.MapGet("products", async (AppDbContext context) =>
            await context.Products.ToListAsync()
             is List<Product> products
               ? Results.Ok(products)
               : Results.NotFound("Não existem Produtos cadastrados."))
        .Produces<Product>(StatusCodes.Status200OK)
        .Produces<Product>(StatusCodes.Status404NotFound)
        .WithName("GetProduct")
        .WithTags("Product");

        app.MapGet("product/{productId}", async (Guid productId, AppDbContext context) =>
           await context.Products.FindAsync(productId)
            is Product product
               ? Results.Ok(product)
               : Results.NotFound("Não existe Produto com o Id informado."))
       .Produces<Product>(StatusCodes.Status200OK)
       .Produces<Product>(StatusCodes.Status404NotFound)
       .WithName("GetProductById")
       .WithTags("Product");

        app.MapPost("product", (AppDbContext context, CreateProductViewModel model) =>
        {
            var product = model.ProductMap();

            if (!model.IsValid)
                return Results.BadRequest(model.Notifications);

            context.Products.Add(product);
            context.SaveChanges();

            return Results.Created($"product/{product.Id}", product);
        }).Produces<Product>(StatusCodes.Status200OK)
          .Produces<Product>(StatusCodes.Status404NotFound)
          .WithName("PostProduct")
          .WithTags("Product");

        app.MapPut("product/{productId}", async (Guid productId, CreateProductViewModel productUpdate, AppDbContext context) =>
        {
            var product = await context.Products.FindAsync(productId);
     
            if (product != null)
            {
                product.Title = productUpdate.Title;
                product.Category = productUpdate.Category;
                product.Description = productUpdate.Description;
                product.Price = productUpdate.Price;
                product.Images = productUpdate.Images;
                product.UpdateDate = DateTime.Now;

                context.Products.Update(product);
                context.SaveChanges();

                return Results.Ok($"Produto com Id: {productId} atualizado com sucesso.");
            }

            return Results.NotFound("Não existe Produto com o Id informado.");
        }).Produces<Product>(StatusCodes.Status200OK)
          .Produces<Product>(StatusCodes.Status404NotFound)
          .WithName("PutProductById")
          .WithTags("Product");

        app.MapDelete("product/{productId}", async (Guid productId, AppDbContext context) =>
        {
            var product = await context.Products.FindAsync(productId);

            if (product != null)
            {
                context.Products.Remove(product);
                context.SaveChanges();

                return Results.Ok($"Produto com Id: {productId} deletado com sucesso.");
            }  
                return Results.NotFound("Não existe Produto com o Id informado.");
        }).Produces<Product>(StatusCodes.Status200OK)
             .Produces<Product>(StatusCodes.Status404NotFound)
             .WithName("DeleteProductById")
             .WithTags("Product");

        return app;
    }
}