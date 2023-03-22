using EquipmentManagement.UI;
using EquipmentManagement.UI.Abstractions;
using EquipmentManagement.UI.Authentification;
using EquipmentManagement.UI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthentificationStateProvider>();
builder.Services.AddSingleton<UserCredentialStorage>();
builder.Services.AddScoped<IEquipmentService, EquipmentServiceWithCaching>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddAuthorizationCore();
builder.Services.AddMemoryCache();
var app = builder.Build();

await app.RunAsync();
