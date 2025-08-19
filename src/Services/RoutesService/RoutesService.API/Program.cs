using Microsoft.EntityFrameworkCore;
using RoutesService.API.Data;

var builder = WebApplication.CreateBuilder(args);

// PostgreSQL + NetTopologySuite
builder.Services.AddDbContext<RoutesDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
                         o => o.UseNetTopologySuite()));

// Controller + JSON ayarları
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Gerekirse burada JSON formatlama yapılabilir
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

using (var scope = app.Services.CreateScope())
{
    try
    {
        var context = scope.ServiceProvider.GetRequiredService<RoutesDbContext>();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Hata: {ex.Message}");
        throw;
    }
}

app.Run();
