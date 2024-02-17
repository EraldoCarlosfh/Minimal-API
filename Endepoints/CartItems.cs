using ApiPetChaoBicho.Data;
using ApiPetChaoBicho.Data.ViewModels;
using Microsoft.EntityFrameworkCore;

public static class CartItems
{
    public static WebApplication MapCartItemEndpoints(this WebApplication app)
    {
        app.MapGet("cartItems", async (AppDbContext context) =>
            await context.CartItems.ToListAsync()
             is List<CartItem> cartItems
               ? Results.Ok(cartItems)
               : Results.NotFound("Não existem Items cadastrados."))
        .Produces<CartItem>(StatusCodes.Status200OK)
        .Produces<CartItem>(StatusCodes.Status404NotFound)
        .WithName("GetCartItem")
        .WithTags("CartItem");

        app.MapGet("cartItem/{cartItemId}", async (Guid cartItemId, AppDbContext context) =>
           await context.CartItems.FindAsync(cartItemId)
            is CartItem cartItem
               ? Results.Ok(cartItem)
               : Results.NotFound("Não existe Item com o Id informado."))
       .Produces<User>(StatusCodes.Status200OK)
       .Produces<User>(StatusCodes.Status404NotFound)
       .WithName("GetCartItemById")
       .WithTags("CartItem");

        app.MapPost("cartItem", (AppDbContext context, CreateCartItemViewModel model) =>
        {
            var cartItem = model.CartItemMap();

            if (!model.IsValid)
                return Results.BadRequest(model.Notifications);

            context.CartItems.Add(cartItem);
            context.SaveChanges();

            return Results.Created($"cartItem/{cartItem.Id}", cartItem);
        }).Produces<CartItem>(StatusCodes.Status200OK)
          .Produces<CartItem>(StatusCodes.Status404NotFound)
          .WithName("PostCartItem")
          .WithTags("CartItem");

        app.MapPut("cartItem/{cartItemId}", async (Guid cartItemId, CreateCartItemViewModel productUpdate, AppDbContext context) =>
        {
            var cartItem = await context.CartItems.FindAsync(cartItemId);

            if (cartItem != null)
            {
                cartItem.Product = productUpdate.Product;
                cartItem.Quantity = productUpdate.Quantity;
                cartItem.Price = productUpdate.Price;
                cartItem.Image = productUpdate.Image;
                cartItem.UpdateDate = DateTime.Now;

                context.CartItems.Update(cartItem);
                context.SaveChanges();

                return Results.Ok($"Item com Id: {cartItemId} atualizado com sucesso.");
            }

            return Results.NotFound("Não existe Item com o Id informado.");
        }).Produces<Product>(StatusCodes.Status200OK)
           .Produces<Product>(StatusCodes.Status404NotFound)
           .WithName("PutCartItemById")
           .WithTags("CartItem");

        app.MapDelete("cartItem/{cartItemId}", async (Guid cartItemId, AppDbContext context) =>
        {
            var cartItem = await context.CartItems.FindAsync(cartItemId);

            if (cartItem != null)
            {
                context.CartItems.Remove(cartItem);
                context.SaveChanges();

                return Results.Ok($"Item com Id: {cartItemId} deletado com sucesso.");
            }
            return Results.NotFound("Não existem Item com o Id informado.");
        }).Produces<CartItem>(StatusCodes.Status200OK)
            .Produces<CartItem>(StatusCodes.Status404NotFound)
            .WithName("DeleteCartItemById")
            .WithTags("CartItem");

        return app;
    }
}