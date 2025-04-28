using Microsoft.EntityFrameworkCore;
using Northwind.OrderManagement.Application.Features.Orders.Commands.CreateOrder;
using Northwind.OrderManagement.Infrastructure.Persistence;
using MediatR;
using Microsoft.OpenApi.Models; // Agregar esta directiva using para Swagger
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Builder;
using Northwind.OrderManagement.Application.Features.Orders.Commands.DeteleOrder;
using Northwind.OrderManagement.Application.Features.Orders.Commands.UpdateOrder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.  

builder.Services.AddControllers(); // Necesitamos esto para soportar los Controllers  

// DbContext  
builder.Services.AddDbContext<NorthwindDbContext>(options =>
   options.UseSqlServer(builder.Configuration.GetConnectionString("NorthwindDatabase")));

// MediatR (forma correcta ahora)  
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(CreateOrderCommandHandler).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(DeleteOrderCommandHandler).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(UpdateOrderCommandHandler).Assembly);

});

// Swagger/OpenAPI  
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Northwind.OrderManagement.API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.  
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwagger();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers(); // ¡Importante para los endpoints de Controllers!  

app.Run();
