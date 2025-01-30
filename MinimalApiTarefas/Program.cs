using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MinimalApiTarefas.Data;
using MinimalApiTarefas.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseInMemoryDatabase("TarefasDb");
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/TarefaEntity", async (AppDbContext db) =>
{
    return await db.Tarefas.ToListAsync();
});

app.MapGet("/TarefaEntity/{id}", async (int id, AppDbContext db) =>
{
    return await db.Tarefas.FindAsync(id) is TarefaEntity tarefa ? Results.Ok(tarefa) : Results.NotFound();
});

app.MapGet("/TarefaEntity/concluidas", async (AppDbContext db) =>
    await db.Tarefas.Where(c => c.IsConcluida == true).ToListAsync());

app.MapPost($"/{nameof(TarefaEntity)}", async (TarefaEntity tarefa, AppDbContext db) =>
{
    db.Add(tarefa);
    await db.SaveChangesAsync();
    return Results.Created($"/{nameof(TarefaEntity)}/{tarefa.Id}", tarefa);
});
 

app.MapPut("/TarefasEntity/{id:int}", async (int id, TarefaEntity inputTarefa, AppDbContext db) =>
{
    TarefaEntity tarefa = await db.Tarefas.FindAsync(id);

    if (tarefa is null) return Results.NotFound();

    tarefa.Nome = inputTarefa.Nome;
    tarefa.IsConcluida = inputTarefa.IsConcluida;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/TarefasEntity/{id:int}", async (int id, AppDbContext db) =>
{
    TarefaEntity tarefa = await db.Tarefas.FindAsync(id);

    if (tarefa is null) return Results.NotFound();

    db.Remove(tarefa);
    await db.SaveChangesAsync();

    return Results.Ok();
});

app.Run();


