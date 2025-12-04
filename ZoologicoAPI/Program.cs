using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;

var builder = WebApplication.CreateBuilder(args);

// --- CONFIGURACIÓN DE BASE DE DATOS ---

// Aquí conectamos tu contexto principal directamente a PostgreSQL.
// Asegúrate de que en tu appsettings.json la cadena se llame "PostgresConnection"
builder.Services.AddDbContext<ZoologicoAPIContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")
    ?? throw new InvalidOperationException("Connection string 'PostgresConnection' not found.")));

// --- FIN CONFIGURACIÓN ---

// Add services to the container.
builder.Services.AddControllers();

// Configuración de Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configurar Swagger para que se vea siempre (necesario para Render)
// if (app.Environment.IsDevelopment()) // Comentado para que funcione en producción
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();