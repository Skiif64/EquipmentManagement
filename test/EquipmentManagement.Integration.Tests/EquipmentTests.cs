using EquipmentManagement.Application.Equipments.Create;
using EquipmentManagement.Domain.Models;
using EquipmentManagement.Integration.Tests.Fixtures;
using Microsoft.EntityFrameworkCore;
using SignalRChatApp.IntegrationTests;

namespace EquipmentManagement.Integration.Tests;
public class EquipmentTests : TestBase
{    
    public EquipmentTests(TestApplicationFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task CreateEquipment_ShouldAddToDatabase_WhenEquipmentIdValid()
    {
        await SeedData(new[] { Constants.EquipmentTypes.CreateA() });

        var command = new CreateEquipmentCommand
        {
            Article = "Article",
            Author = "Test user",
            CreatedAt = DateTimeOffset.MinValue,
            SerialNumber = "42",
            TypeId = Constants.EquipmentTypes.TypeAId
        };

        var equipmentId = await Sender.Send(command, default);

        var actual = await Context.Set<Equipment>().SingleOrDefaultAsync(x => x.Id == equipmentId);

        Assert.NotNull(actual);
    }
}
