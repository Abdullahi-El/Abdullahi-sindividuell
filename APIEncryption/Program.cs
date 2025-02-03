var builder = WebApplication.CreateBuilder(args);

// Lägg till tjänster i containern.
// Läs mer om hur man konfigurerar OpenAPI på https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Konfigurera HTTP-begäringspipen.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var sammanfattningar = new[]
{
    "Frysande", "Frisk", "Kylig", "Kall", "Mild", "Varm", "Behaglig", "Het", "Tryckande", "Brännande"
};

app.MapGet("/weatherforecast", () =>
{
    var prognos = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            sammanfattningar[Random.Shared.Next(sammanfattningar.Length)]
        ))
        .ToArray();
    return prognos;
})
.WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Datum, int TemperaturC, string? Sammanfattning)
{
    public int TemperaturF => 32 + (int)(TemperaturC / 0.5556);
}
