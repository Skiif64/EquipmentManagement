using Blazored.LocalStorage;
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

builder.Services.AddHttpClient("Api", client
    => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
    .AddHttpMessageHandler<AuthenticationHttpMessageHandler>();

builder.Services.AddScoped(sp =>
    sp.GetRequiredService<IHttpClientFactory>().CreateClient("Api"));
builder.Services.AddScoped<JwtAuthentificationStateProvider>();
builder.Services.AddScoped<IAuthenticationStateNotifier>(sp =>
    sp.GetRequiredService<JwtAuthentificationStateProvider>());
builder.Services.AddScoped<AuthenticationStateProvider>(sp =>
    sp.GetRequiredService<JwtAuthentificationStateProvider>());
builder.Services.AddScoped<IEquipmentService, EquipmentServiceWithCaching>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IStatusService, StatusService>();
builder.Services.AddScoped<IAuthentificationService, AuthenticationService>();
builder.Services.AddScoped<AuthenticationHttpMessageHandler>();
builder.Services.AddAuthorizationCore();
builder.Services.AddMemoryCache();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<ITokenStorage, TokenStorage>();
var app = builder.Build();

await app.RunAsync();
