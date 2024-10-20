using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using WebApplication1.Models;

var builder = WebApplication.CreateBuilder(args);

// Cors

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalHost",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod()
            );
});


// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer("name=ConnectionnString:DefaultConnection"));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseCors("AllowLocalHost"); // Apply the Cors policy

app.UseAuthorization();

app.MapControllers();

app.Run();
