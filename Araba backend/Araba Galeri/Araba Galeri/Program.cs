using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Kestrel'i doðrudan yapýlandýrarak, belirli portlarda dinleme ayarlarýný yapýyoruz.
builder.WebHost.ConfigureKestrel(options =>
{
    // HTTP için 5118 portu
    options.ListenLocalhost(5118);

    // HTTPS için 7259 portu
    options.ListenLocalhost(7259, listenOptions =>
    {
        listenOptions.UseHttps(); // Geliþtirme aþamasýnda self-signed sertifika kullanabilirsiniz.
    });
});

// Gerekli servisleri ekleyelim.
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policyBuilder =>
    {
        policyBuilder.AllowAnyOrigin()
                     .AllowAnyMethod()
                     .AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();

// Controller endpoint'lerini eþliyoruz.
app.MapControllers();

// Test amaçlý kök endpoint
app.MapGet("/", () =>
    "API çalýþýyor. HTTP: http://localhost:5118 | HTTPS: https://localhost:7259");

app.Run();
