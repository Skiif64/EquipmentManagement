using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.DAL;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Respawn;

using Testcontainers.PostgreSql;

namespace EquipmentManagement.Integration.Tests.Fixtures;
public class TestApplicationFactory
    : WebApplicationFactory<Program>,    
    IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
        .WithDatabase("EquipmentManagement")
        .WithUsername("postgres")
        .WithPassword("1234")
        .Build();

    private NpgsqlConnection _connection = null!;
    private Respawner _respawner = null!;

    public Task InitializeAsync() //Respawner initialising won't work in Initialize async
        => Task.CompletedTask;

    public async Task ResetDatabaseAsync()
    {
        await _respawner.ResetAsync(_connection);
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        InitializeDb();
        builder.ConfigureTestServices(services =>
        {
            var descriptor = services
            .SingleOrDefault(x => x.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

            if (descriptor is not null)
                services.Remove(descriptor);

            services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(opt => opt
            .UseNpgsql(_dbContainer.GetConnectionString()));

            var provider = services.BuildServiceProvider();
            using (var scope = provider.CreateScope())
            {
                using (var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>())
                {                    
                    context.Database.EnsureCreated();
                }
            }
            if (_respawner is null)
                InitializeRespawner();
        });
    }

    private void InitializeRespawner()
    {
        Task.Run(async () =>
        {
            _respawner = await Respawner.CreateAsync(_connection, new RespawnerOptions
            {
                DbAdapter = DbAdapter.Postgres,
                SchemasToInclude = new[]
                {
                    "public"
                },
            });
        }).Wait();
    }

    private void InitializeDb()
    {
        Task.Run(async () =>
        {
            await _dbContainer.StartAsync();
            _connection = new NpgsqlConnection(_dbContainer.GetConnectionString());
            await _connection.OpenAsync();
        }).Wait();
    }

    async Task IAsyncLifetime.DisposeAsync()
    {
        if(_dbContainer is not null)
            await _dbContainer.StopAsync();
        if (_connection is not null)
            await _connection.DisposeAsync();
        await DisposeAsync();
    }
}
