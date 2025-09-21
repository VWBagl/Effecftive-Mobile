using AdPlat.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSingleton<IAdPlatService, AdPlatService>();

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
