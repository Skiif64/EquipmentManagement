using Blazored.LocalStorage;
using Blazored.Modal;
using EquipmentManagement.UI;
using EquipmentManagement.UI.Abstractions;
using EquipmentManagement.UI.Authentification;
using EquipmentManagement.UI.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("Api", client
    => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
    .AddHttpMessageHandler<RefreshTokenHttpMessageHandler>()
    .AddHttpMessageHandler<AuthenticationHttpMessageHandler>()
    .AddHttpMessageHandler<RedirectToLoginHttpMessageHandler>(); 

builder.Services.AddScoped(sp =>
    sp.GetRequiredService<IHttpClientFactory>().CreateClient("Api"));

builder.Services.AddHttpClient("RefreshAccessToken", cfg =>
cfg.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

builder.Services.AddScoped<JwtAuthentificationStateProvider>();
builder.Services.AddScoped<IAuthenticationStateNotifier>(sp =>
    sp.GetRequiredService<JwtAuthentificationStateProvider>());
builder.Services.AddScoped<AuthenticationStateProvider>(sp =>
    sp.GetRequiredService<JwtAuthentificationStateProvider>());
builder.Services.AddScoped<IEquipmentService, EquipmentServiceWithCaching>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IStatusService, StatusService>();
builder.Services.AddScoped<IEquipmentRecordService, EquipmentRecordService>();
builder.Services.AddScoped<IAuthentificationService, AuthenticationService>();
builder.Services.AddScoped<AuthenticationHttpMessageHandler>();
builder.Services.AddScoped<RefreshTokenHttpMessageHandler>();
builder.Services.AddScoped<RedirectToLoginHttpMessageHandler>();
builder.Services.AddScoped<IJwtTokenRefresher, JwtTokenRefresher>();
builder.Services.AddAuthorizationCore();
builder.Services.AddMemoryCache();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddBlazoredModal();
builder.Services.AddScoped<ITokenStorage, TokenStorage>();
builder.Services.AddSingleton<IAlertService, AlertService>();
var app = builder.Build();

await app.RunAsync();
