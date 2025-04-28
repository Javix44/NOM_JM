
using Microsoft.EntityFrameworkCore;
using Northwind.OrderManagement.Application.Features.Orders.Commands.CreateOrder;
using Northwind.OrderManagement.Infrastructure.Persistence;
using MediatR;
using Microsoft.OpenApi.Models;
using Northwind.OrderManagement.Application.Features.Orders.Commands.DeteleOrder;
using Northwind.OrderManagement.Application.Features.Orders.Commands.UpdateOrder;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

// Configuración de servicios
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policyBuilder =>
    {
        policyBuilder.AllowAnyOrigin()
                     .AllowAnyMethod()
                     .AllowAnyHeader();
    });
});

// Agregar servicios al contenedor
builder.Services.AddControllers(); // Necesario para soportar los Controllers

// Configuración de DbContext
builder.Services.AddDbContext<NorthwindDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("NorthwindDatabase")));

// Configuración de MediatR
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(CreateOrderCommandHandler).Assembly);
});

// Configuración de Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Northwind.OrderManagement.API", Version = "v1" });
});

var app = builder.Build();

// Configuración del pipeline de solicitudes HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
}

app.UseHttpsRedirection();

// Usar CORS antes de otras configuraciones
app.UseCors("AllowAll");

app.UseRouting();

app.UseAuthorization(); // Mover entre UseRouting y MapControllers para cumplir con ASP0001

app.MapControllers(); // Importante para los endpoints de Controllers

app.Run();