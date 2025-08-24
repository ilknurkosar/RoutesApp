using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NetTopologySuite.IO.Converters;
using RoutesService.API.Data;
using RoutesService.API.Mapping;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// PostgreSQL + NetTopologySuite
builder.Services.AddDbContext<RoutesDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
                         o => o.UseNetTopologySuite()));

builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
        options.JsonSerializerOptions.Converters.Add(new GeoJsonConverterFactory()); 
    });

// 🔑 CORS ekleme (React bağlanabilsin diye)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy => policy.WithOrigins("http://localhost:3000")
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger (sadece Development)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// CORS middleware
app.UseCors("AllowReactApp");

app.UseAuthorization();

app.MapControllers();



app.Run();