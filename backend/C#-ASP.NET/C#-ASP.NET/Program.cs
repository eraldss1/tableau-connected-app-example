using C__ASP.NET.Utils;
using Microsoft.AspNetCore.Mvc;


// Load Environment Variable
var root = Directory.GetCurrentDirectory();
var dotenv = Path.Combine(root, ".env");
DotEnv.Load(dotenv);

// -
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Services for controller
builder.Services.AddControllers();

// Services for CORS
builder.Services.AddCors();
builder.Services.AddMvc();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure CORS for app
app.UseCors(options => options.WithOrigins("*").AllowAnyMethod());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
