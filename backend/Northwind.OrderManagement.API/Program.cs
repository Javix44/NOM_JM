using Microsoft.EntityFrameworkCore;
using Northwind.OrderManagement.Application.Features.Orders.Commands.CreateOrder;
using Northwind.OrderManagement.Infrastructure.Persistence;
using Microsoft.OpenApi.Models;
using QuestPDF.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Configuracion de servicios
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

// Configuracion de DbContext
builder.Services.AddDbContext<NorthwindDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("NorthwindDatabase")));

// Configuracion de MediatR
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(CreateOrderCommandHandler).Assembly);
});

// Configuracion de Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Northwind.OrderManagement.API", Version = "v1" });
});

//Licencia de Uso de Generacion de PDF
QuestPDF.Settings.License = LicenseType.Community;
QuestPDF.Settings.EnableDebugging = true;

var app = builder.Build();

// Configuracion del pipeline de solicitudes HTTP
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Usar CORS antes de otras configuraciones
app.UseCors("AllowAll");

app.UseRouting();

app.UseAuthorization();  

app.MapControllers(); // Importante para los endpoints de Controllers

app.Run();