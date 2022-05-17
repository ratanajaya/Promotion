using Microsoft.EntityFrameworkCore;
using PromotionAPI;
using PromotionAPI.Models;
using PromotionAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton(AppSettingJson.GetGlobalVariable());
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(AppSettingJson.GetConnectionString()));
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
