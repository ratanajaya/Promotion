using Microsoft.EntityFrameworkCore;
using PromotionAPI;
using PromotionAPI.Models;
using PromotionAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton(AppSettingJson.GetGlobalVariable());
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(AppSettingJson.GetConnectionString()));
builder.Services.AddControllers();
builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder => {
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseCors("MyPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
