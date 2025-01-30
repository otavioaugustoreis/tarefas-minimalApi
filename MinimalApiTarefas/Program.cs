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

app.MapGet("/", () =>
{
    return "Olá mundo";
});

app.MapGet("frases", async  () =>
{
    return await new HttpClient().GetStringAsync("https://ron-swanson-quotes.herokuapp.com/v2/quotes");
});


app.MapGet($"/{nameof(TarefaEntity)}", async (AppDbConte) =>
{
    return await new HttpClient().GetStringAsync("https://ron-swanson-quotes.herokuapp.com/v2/quotes");
});

app.Run();


