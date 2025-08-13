using Application.DTOs.DocumentType;
using Application.UseCases.DocumentType;
using Application.Validators.DocumentType;
using Core.Interfaces;
using DependencyInjection.Dependency.Modules;
using FluentValidation;
using GlueMark.Middleware;
using Infrastructure.Dependency.Modules;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDocumentTypeServices();
builder.Services.AddCurrencyServices();


builder.Services.AddScoped<ISqlConnectionFactory, SqlConnectionFactory>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();



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
