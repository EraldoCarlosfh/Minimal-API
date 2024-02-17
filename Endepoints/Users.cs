using ApiPetChaoBicho.Data;
using ApiPetChaoBicho.Data.ViewModels;
using Microsoft.EntityFrameworkCore;

public static class Users
{
    public static WebApplication MapUserEndpoints(this WebApplication app)
    {
        app.MapGet("users", async (AppDbContext context) =>
            await context.Users.ToListAsync()
             is List<User> users
               ? Results.Ok(users)
               : Results.NotFound("Não existem Usuários cadastrados."))
        .Produces<User>(StatusCodes.Status200OK)
        .Produces<User>(StatusCodes.Status404NotFound)
        .WithName("GetUsers")
        .WithTags("User");

        app.MapGet("user/{userId}", async (Guid userId, AppDbContext context) =>
           await context.Users.FindAsync(userId)
            is User user
               ? Results.Ok(user)
               : Results.NotFound("Não existe Usuário com o Id informado."))
       .Produces<User>(StatusCodes.Status200OK)
       .Produces<User>(StatusCodes.Status404NotFound)
       .WithName("GetUserById")
       .WithTags("User");

        app.MapPost("user", (AppDbContext context, CreateUserViewModel model) => 
        {
            var user = model.UserMap();

            if (!model.IsValid)
                return Results.BadRequest(model.Notifications);

            context.Users.Add(user);
            context.SaveChanges();

            return Results.Created($"user/{user.Id}", user);
        }).Produces<User>(StatusCodes.Status200OK)
          .Produces<User>(StatusCodes.Status404NotFound)
          .WithName("PostUser")
          .WithTags("User");

        app.MapPut("user/{userId}", async (Guid userId, CreateUserViewModel userUpdate, AppDbContext context) =>
        {
            var user = await context.Users.FindAsync(userId);

            if (user != null)
            {
                user.Name = userUpdate.Name;
                user.Email = userUpdate.Email;
                user.Document = userUpdate.Document;
                user.UpdateDate = DateTime.Now;

                context.Users.Update(user);
                context.SaveChanges();

                return Results.Ok($"Usuário com Id: {userId} atualizado com sucesso.");
            }

            return Results.NotFound("Não existe Usuário com o Id informado.");
        }).Produces<User>(StatusCodes.Status200OK)
          .Produces<User>(StatusCodes.Status404NotFound)
          .WithName("PutUserById")
          .WithTags("User");

        app.MapDelete("user/{userId}", async (Guid userId, AppDbContext context) =>
        {
            var user = await context.Users.FindAsync(userId);

            if (user != null)
            {
                context.Users.Remove(user);
                context.SaveChanges();

                return Results.Ok($"Usuário com Id: {userId} deletado com sucesso.");
            }

            return Results.NotFound("Não existe Usuário com o Id informado.");
        }).Produces<User>(StatusCodes.Status200OK)
            .Produces<User>(StatusCodes.Status404NotFound)
            .WithName("DeleteUserById")
            .WithTags("User");

        return app;
    }
}


//namespace MinimalAPI;

//using Dapper;
//using Microsoft.Data.Sqlite;

//public class TodosModule : ICarterModule
//{
//    public void AddRoutes(IEndpointRouteBuilder app)
//    {
//        app.MapGet("/api/todos", GetTodos);
//        app.MapGet("/api/todos/{id}", GetTodo);
//        app.MapPost("/api/todos", CreateTodo);
//        app.MapPut("/api/todos/{id}/mark-complete", MarkComplete);
//        app.MapDelete("/api/todos/{id}", DeleteTodo);
//    }
//    private static async Task<IResult> GetTodo(int id, SqliteConnection db) =>
//        await db.QuerySingleOrDefaultAsync<Todo>(
//            "SELECT * FROM Todos WHERE Id = @id", new { id })
//            is Todo todo
//                ? Results.Ok(todo)
//                : Results.NotFound();

//    private async Task<IEnumerable<Todo>> GetTodos(SqliteConnection db) =>
//        await db.QueryAsync<Todo>("SELECT * FROM Todos");

//    private static async Task<IResult> CreateTodo(Todo todo, SqliteConnection db)
//    {
//        var newTodo = await db.QuerySingleAsync<Todo>(
//            "INSERT INTO Todos(Title, IsComplete) Values(@Title, @IsComplete) RETURNING * ", todo);

//        return Results.Created($"/todos/{newTodo.Id}", newTodo);
//    }
//    private static async Task<IResult> DeleteTodo(int id, SqliteConnection db) =>
//        await db.ExecuteAsync(
//            "DELETE FROM Todos WHERE Id = @id", new { id }) == 1
//            ? Results.NoContent()
//            : Results.NotFound();
//    private static async Task<IResult> MarkComplete(int id, SqliteConnection db) =>
//        await db.ExecuteAsync(
//            "UPDATE Todos SET IsComplete = true WHERE Id = @Id", new { Id = id }) == 1
//            ? Results.NoContent()
//            : Results.NotFound();
//}

//public class Todo
//{
//    public int Id { get; set; }
//    public string? Title { get; set; }
//    public bool IsComplete { get; set; }
//}