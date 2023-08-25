using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Domain.Models.Base;
using EquipmentManagement.Integration.Tests.Fixtures;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace SignalRChatApp.IntegrationTests;

public abstract class TestBase : IAsyncLifetime, IClassFixture<TestApplicationFactory>
{
    protected readonly TestApplicationFactory Factory;
    protected readonly IServiceScope Scope;
    protected readonly ISender Sender;
    protected readonly IApplicationDbContext Context;
    public TestBase(TestApplicationFactory factory)
    {
        Factory = factory;
        Scope = Factory.Services.CreateScope();
        Sender = Scope.ServiceProvider.GetRequiredService<ISender>();
        Context = Scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();
    }

    protected virtual async Task SeedData<TEntity>(IEnumerable<TEntity> data)
        where TEntity : BaseModel
    {        
        await Context.Set<TEntity>().AddRangeAsync(data);
        await Context.SaveChangesAsync(default);
    }

    public async Task DisposeAsync()
    {
        await Factory.ResetDatabaseAsync();
        Scope.Dispose();
    }

    public Task InitializeAsync()
        => Task.CompletedTask;
}
