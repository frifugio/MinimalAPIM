using Microsoft.EntityFrameworkCore;
using MinimalAPIM_Test.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<RestaurantOrderDb>(opt => opt.UseInMemoryDatabase("RestaurantOrder"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/getCheck/{tableNumber}", async (RestaurantOrderDb db, string tableNumber) =>
{
    return await db.Orders.Where(o => o.TableNumber == tableNumber).Include(m => m.Meals).ToListAsync();
})
.WithName("GetCheck");

app.MapPost("/newOrder", async (Order order, RestaurantOrderDb db) =>
{
    db.Orders.Add(order);
    await db.SaveChangesAsync();

    return Results.Created($"/getCheck/{order.TableNumber}", order);
})
.WithName("SubmitNewOrder")
.Produces(StatusCodes.Status201Created);

app.MapDelete("/clearTable/{tableNumber}", async (RestaurantOrderDb db, string tableNumber) =>
{
    var tableOrders = await db.Orders.Where(o => o.TableNumber == tableNumber).ToListAsync();
    if(tableOrders.Any())
    {
        foreach (var order in tableOrders)
        {
            db.Orders.Remove(order);
        }
        
        await db.SaveChangesAsync();
        return Results.Ok();
    }
    else
        return Results.NotFound();
})
.WithName("ClearTable")
.Produces(StatusCodes.Status200OK).Produces(StatusCodes.Status404NotFound);


app.Run();