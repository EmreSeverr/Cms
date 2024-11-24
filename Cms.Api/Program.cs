using Cms.Api;
using Cms.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMapster();

builder.Services.ConfigureDependencyInjections();
builder.ConfigureDbContext();

builder.Services.ConfigureCors();

builder.ConfigureRedis();

var app = builder.Build();


//if (app.Environment.IsDevelopment()) //Test icin kapatildi.
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseStaticFiles();

app.MigrateAsync().Wait();

app.UseCors("CmsCors");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
