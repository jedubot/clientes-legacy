using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Clientes.WebApi.Interfaces;
using Clientes.WebApi.Models;
using Clientes.WebApi.Repositories;
using Clientes.WebApi.Repositories.EF;
using Clientes.WebApi.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Configure connection strings for Entity Framework 6
System.Configuration.ConfigurationManager.ConnectionStrings.Clear();
var connectionString = builder.Configuration.GetConnectionString("ClientesDbContext");
if (!string.IsNullOrEmpty(connectionString))
{
    System.Configuration.ConfigurationManager.ConnectionStrings.Add(
        new System.Configuration.ConnectionStringSettings("ClientesDbContext", connectionString, "System.Data.SqlClient"));
}

// Add services to the container
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        options.SerializerSettings.Formatting = Formatting.Indented;
    });

// Register Entity Framework DbContext
builder.Services.AddScoped<ClientesDbContext>();

// Register application services (replaces Autofac registrations)
builder.Services.AddScoped<IRepository<Cliente>, Repository<Cliente>>();
builder.Services.AddScoped<IClienteService, ClienteService>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();
app.MapControllers();

app.Run();
