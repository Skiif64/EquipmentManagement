using EquipmentManagement.Application;
using EquipmentManagement.Auth;
using EquipmentManagement.DAL;
using EquipmentManagement.WebApi;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<WebApiMappingProfile>();
    cfg.AddProfile<ApplicationMappingProfile>();
});
builder.Services.AddApplication();
builder.Services.AddDataAccess(builder.Configuration);
builder.Services.AddAuth(builder.Configuration);
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();
builder.Services.AddCors(opt => opt.AddPolicy("CorsPolicy", policy => policy.AllowAnyOrigin()));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseWebAssemblyDebugging();
}

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();
app.UseCors("CorsPolicy");

app.MapControllers();
app.MapRazorPages();
app.MapFallbackToFile("index.html");
app.Run();
