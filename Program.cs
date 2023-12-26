using ICustomer_API;
using ICustomer_API.Models;
using ICustomer_API.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json");


builder.Services.AddControllers();
//builder.Services.AddDbContext<ICustomerDbContext>(options => options.UseInMemoryDatabase("ICustomerDb"));
builder.Services.AddDbContext<ICustomerDbContext>(options =>
       options.UseSqlServer(builder.Configuration.GetConnectionString("ICustomerDatabase")));
builder.Services.AddScoped<CustomHttpClient>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

var app = builder.Build();

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
